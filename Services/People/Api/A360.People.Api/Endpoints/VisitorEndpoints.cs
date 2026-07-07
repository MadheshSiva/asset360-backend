using System.Globalization;
using A360.Email;
using A360.People.Api.Contracts;
using A360.People.Api.Settings;
using A360.People.Api.Validation;
using A360.People.Repository.Repositories;
using A360.Repository.Repositories;
using VisitorEntity = A360.People.Domain.Entities.Visitor;

namespace A360.People.Api.Endpoints;

public static class VisitorEndpoints
{
    public static RouteGroupBuilder MapVisitorEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/visitors")
            .WithTags("Visitors");

        group.MapGet("", GetVisitorsAsync)
            .WithName("GetVisitors");

        group.MapGet("/{id}", GetVisitorByIdAsync)
            .WithName("GetVisitorById");

        group.MapGet("/authcode/{authCode}", GetVisitorByAuthCodeAsync)
            .WithName("GetVisitorByAuthCode");

        group.MapPost("", CreateVisitorAsync)
            .WithName("CreateVisitor");

        group.MapPut("/{id}", UpdateVisitorAsync)
            .WithName("UpdateVisitor");

        group.MapDelete("/{id}", DeleteVisitorAsync)
            .WithName("DeleteVisitor");

        return group;
    }

    private static async Task<IResult> GetVisitorsAsync(
        IVisitorRepository repository,
        CancellationToken cancellationToken)
    {
        var visitors = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            visitors.Select(VisitorResponse.FromEntity));
    }

    private static async Task<IResult> GetVisitorByIdAsync(
        string id,
        IVisitorRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid visitor id." });
        }

        var visitor = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return visitor is null
            ? Results.NotFound()
            : Results.Ok(VisitorResponse.FromEntity(visitor));
    }

    private static async Task<IResult> GetVisitorByAuthCodeAsync(
        string authCode,
        IVisitorRepository repository,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(authCode))
        {
            return Results.BadRequest(
                new { message = "Authorization code is required." });
        }

        var visitor = await repository.GetByAuthCodeAsync(
            authCode,
            cancellationToken);

        if (visitor is null)
        {
            return Results.NotFound(
                new { message = "No visitor found for the given authorization code." });
        }

        var now = DateTime.UtcNow;

        if (now > visitor.EndDate)
        {
            return Results.Ok(
                VisitorAuthCodeResponse.Failure(
                    "Authorization code has expired. Start and end date have ended."));
        }

        return Results.Ok(
            VisitorAuthCodeResponse.Success(visitor));
    }

    private static async Task<IResult> CreateVisitorAsync(
        CreateVisitorRequest request,
        IVisitorRepository repository,
        IEmailService emailService,
        VisitorNotificationSettings visitorNotificationSettings,
        ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var clientId = request.ClientId ?? string.Empty;
        var email = request.Email!.Trim();

        var existingVisitorsForEmail = await repository.GetByEmailAsync(
            clientId,
            email,
            cancellationToken);

        var hasOverlappingStay = existingVisitorsForEmail.Any(existing =>
            existing.StartDate <= request.EndDate && existing.EndDate >= request.StartDate);

        if (hasOverlappingStay)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["Email"] =
                [
                    "A visitor with this email is already registered for an overlapping start and end date."
                ]
            });
        }

        var visitor = request.ToEntity();
        visitor.IDNumber = existingVisitorsForEmail.FirstOrDefault()?.IDNumber ?? GenerateSixDigitCode();
        visitor.AuthCode = GenerateSixDigitCode();

        var created = await repository.CreateAsync(
            visitor,
            cancellationToken);

        await TrySendVisitorRegistrationEmailAsync(
            created,
            emailService,
            visitorNotificationSettings,
            loggerFactory,
            cancellationToken);

        await TrySendHostNotificationEmailAsync(
            created,
            emailService,
            visitorNotificationSettings,
            loggerFactory,
            cancellationToken);

        return Results.Created(
            $"/api/visitors/{created.Id}",
            VisitorResponse.FromEntity(created));
    }

    private static string GenerateSixDigitCode()
    {
        return Random.Shared.Next(100000, 1000000).ToString(CultureInfo.InvariantCulture);
    }

    private static async Task TrySendVisitorRegistrationEmailAsync(
        VisitorEntity visitor,
        IEmailService emailService,
        VisitorNotificationSettings visitorNotificationSettings,
        ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(visitor.Email))
        {
            return;
        }

        try
        {
            var body = BuildVisitorRegistrationEmailBody(visitor, visitorNotificationSettings.PortalUrl);

            await emailService.SendEmailAsync(
                visitor.Email,
                "Your Purple IQ Visitor Registration Details",
                body,
                cancellationToken);
        }
        catch (Exception ex)
        {
            loggerFactory.CreateLogger("VisitorEndpoints")
                .LogError(ex, "Failed to send visitor registration email to {Email}", visitor.Email);
        }
    }

    private static async Task TrySendHostNotificationEmailAsync(
        VisitorEntity visitor,
        IEmailService emailService,
        VisitorNotificationSettings visitorNotificationSettings,
        ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(visitor.HostPersonEmail))
        {
            return;
        }

        try
        {
            var body = BuildHostNotificationEmailBody(visitor, visitorNotificationSettings.PortalUrl);

            await emailService.SendEmailAsync(
                visitor.HostPersonEmail,
                "New Visitor Registered for Your Visit",
                body,
                cancellationToken);
        }
        catch (Exception ex)
        {
            loggerFactory.CreateLogger("VisitorEndpoints")
                .LogError(ex, "Failed to send host notification email to {Email}", visitor.HostPersonEmail);
        }
    }

    private static string BuildHostNotificationEmailBody(VisitorEntity visitor, string portalUrl)
    {
        return
            $"""
            Hi {visitor.HostPerson},

            A visitor has registered for a visit with you. Here are the details:

            Visitor Panel URL: {portalUrl}
            Visitor ID: {visitor.Id}
            FirstName: {visitor.Firstname}
            LastName: {visitor.Lastname}
            Email: {visitor.Email}
            Department: {visitor.Dept}
            PhoneNo: {visitor.PhoneNo}
            Company: {visitor.Company}
            visitor company: {visitor.VisitorCompany}
            StartDate: {visitor.StartDate.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)}
            EndDate: {visitor.EndDate.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)}

            Best regards,
            Purple IQ team!
            """;
    }

    private static string BuildVisitorRegistrationEmailBody(VisitorEntity visitor, string portalUrl)
    {
        return
            $"""
            Hi {visitor.Firstname} {visitor.Lastname},

            Here are your details:

            Visitor Panel URL: {portalUrl}
            Visitor ID: {visitor.Id}
            FirstName: {visitor.Firstname}
            LastName: {visitor.Lastname}
            Department: {visitor.Dept}
            PhoneNo: {visitor.PhoneNo}
            Company: {visitor.Company}
            Authorization code: {visitor.AuthCode}
            visitor company: {visitor.VisitorCompany}
            StartDate: {visitor.StartDate.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)}
            EndDate: {visitor.EndDate.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)}

            Best regards,
            Purple IQ team!
            """;
    }

    private static async Task<IResult> UpdateVisitorAsync(
        string id,
        UpdateVisitorRequest request,
        IVisitorRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid visitor id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var visitor = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (visitor is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(visitor);

        var updated = await repository.UpdateAsync(
            id,
            visitor,
            cancellationToken);

        return updated
            ? Results.Ok(VisitorResponse.FromEntity(visitor))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteVisitorAsync(
        string id,
        IVisitorRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid visitor id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}