using A360.Repository.Repositories;
using A360.VisitorManagement.Api.Contracts;
using A360.VisitorManagement.Api.Validation;
using A360.VisitorManagement.Repository.Repositories;

namespace A360.VisitorManagement.Api.Endpoints;

public static class EmailTemplateEndpoints
{
    public static RouteGroupBuilder MapEmailTemplateEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/emailtemplates")
            .WithTags("EmailTemplates");

        group.MapGet("", GetAllAsync)
            .WithName("GetEmailTemplates");

        group.MapGet("/{id}", GetByIdAsync)
            .WithName("GetEmailTemplateById");

        group.MapPost("", CreateAsync)
            .WithName("CreateEmailTemplate");

        group.MapPut("/{id}", UpdateAsync)
            .WithName("UpdateEmailTemplate");

        group.MapDelete("/{id}", DeleteAsync)
            .WithName("DeleteEmailTemplate");

        return group;
    }

    private static async Task<IResult> GetAllAsync(
        IEmailTemplateRepository repository,
        CancellationToken cancellationToken)
    {
        var templates = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            templates.Select(EmailTemplateResponse.FromEntity));
    }

    private static async Task<IResult> GetByIdAsync(
        string id,
        IEmailTemplateRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid email template id." });
        }

        var template = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return template is null
            ? Results.NotFound()
            : Results.Ok(EmailTemplateResponse.FromEntity(template));
    }

    private static async Task<IResult> CreateAsync(
        CreateEmailTemplateRequest request,
        IEmailTemplateRepository repository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var existing = await repository.GetByNameAsync(
            request.Name!.Trim(),
            cancellationToken);

        if (existing is not null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["Name"] =
                [
                    "An email template with this name already exists."
                ]
            });
        }

        var created = await repository.CreateAsync(
            request.ToEntity(),
            cancellationToken);

        return Results.Created(
            $"/api/emailtemplates/{created.Id}",
            EmailTemplateResponse.FromEntity(created));
    }

    private static async Task<IResult> UpdateAsync(
        string id,
        UpdateEmailTemplateRequest request,
        IEmailTemplateRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid email template id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var template = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (template is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(template);

        var updated = await repository.UpdateAsync(
            id,
            template,
            cancellationToken);

        return updated
            ? Results.Ok(EmailTemplateResponse.FromEntity(template))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteAsync(
        string id,
        IEmailTemplateRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid email template id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}
