using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using A360.Media.Client;
using A360.Repository.Repositories;
using A360.VisitorManagement.Api.Contracts;
using A360.VisitorManagement.Api.Validation;
using A360.VisitorManagement.Repository.Repositories;
using RegistrationDocumentEntity = A360.VisitorManagement.Domain.Entities.VisitorRegistrationDocument;

namespace A360.VisitorManagement.Api.Endpoints;

public static class VisitorRegistrationEndpoints
{
    private static readonly Dictionary<string, (string CanonicalType, string Category)> DocumentTypeMap = new()
    {
        ["photo"] = ("Photo", "VisitorPhoto"),
        ["passport"] = ("Passport", "VisitorPassport"),
        ["visa"] = ("Visa", "VisitorVisa"),
        ["supportingdocs"] = ("Supporting Docs", "VisitorSupportingDocs"),
        ["nationalid"] = ("National ID", "VisitorNationalId")
    };

    public static RouteGroupBuilder MapVisitorRegistrationEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/visitorregistrations")
            .WithTags("VisitorRegistrations");

        group.MapGet("", GetAllAsync)
            .WithName("GetVisitorRegistrations");

        group.MapGet("/{id}", GetByIdAsync)
            .WithName("GetVisitorRegistrationById");

        group.MapGet("/type/{visitorType}", GetByVisitorTypeAsync)
            .WithName("GetVisitorRegistrationsByVisitorType");

        group.MapPost("", CreateAsync)
            .WithName("CreateVisitorRegistration");

        group.MapPut("/{id}", UpdateAsync)
            .WithName("UpdateVisitorRegistration");

        group.MapDelete("/{id}", DeleteAsync)
            .WithName("DeleteVisitorRegistration");

        group.MapPost("/{id}/documents/upload", UploadDocumentAsync)
            .WithName("UploadVisitorRegistrationDocument")
            .DisableAntiforgery();

        return group;
    }

    private static async Task<IResult> GetAllAsync(
        IVisitorRegistrationRepository repository,
        CancellationToken cancellationToken)
    {
        var registrations = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            registrations.Select(VisitorRegistrationResponse.FromEntity));
    }

    private static async Task<IResult> GetByIdAsync(
        string id,
        IVisitorRegistrationRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid visitor registration id." });
        }

        var registration = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return registration is null
            ? Results.NotFound()
            : Results.Ok(VisitorRegistrationResponse.FromEntity(registration));
    }

    private static async Task<IResult> GetByVisitorTypeAsync(
        string visitorType,
        IVisitorRegistrationRepository repository,
        CancellationToken cancellationToken)
    {
        var registrations = await repository.GetByVisitorTypeAsync(
            visitorType,
            cancellationToken);

        return Results.Ok(
            registrations.Select(VisitorRegistrationResponse.FromEntity));
    }

    private static async Task<IResult> CreateAsync(
        CreateVisitorRegistrationRequest request,
        IVisitorRegistrationRepository repository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var email = request.Email!.Trim();

        var existing = await repository.GetByEmailAsync(
            email,
            cancellationToken);

        if (existing is not null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["Email"] =
                [
                    "A visitor registration with this email already exists."
                ]
            });
        }

        var registration = request.ToEntity();
        registration.Password = GenerateSixDigitCode();

        var created = await repository.CreateAsync(
            registration,
            cancellationToken);

        return Results.Created(
            $"/api/visitorregistrations/{created.Id}",
            VisitorRegistrationResponse.FromEntity(created));
    }

    private static async Task<IResult> UpdateAsync(
        string id,
        UpdateVisitorRegistrationRequest request,
        IVisitorRegistrationRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid visitor registration id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var registration = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (registration is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(registration);

        var updated = await repository.UpdateAsync(
            id,
            registration,
            cancellationToken);

        return updated
            ? Results.Ok(VisitorRegistrationResponse.FromEntity(registration))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteAsync(
        string id,
        IVisitorRegistrationRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid visitor registration id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }

    private static async Task<IResult> UploadDocumentAsync(
        string id,
        IFormFile file,
        [FromForm] string documentType,
        IVisitorRegistrationRepository repository,
        IMediaStorageClient mediaStorageClient,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid visitor registration id." });
        }

        if (file.Length == 0)
        {
            return Results.BadRequest(
                new { message = "File is required." });
        }

        var key = NormalizeDocumentTypeKey(documentType);

        if (!DocumentTypeMap.TryGetValue(key, out var mapping))
        {
            return Results.BadRequest(new
            {
                message = "documentType must be one of: Photo, Passport, Visa, Supporting Docs, National ID."
            });
        }

        var registration = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (registration is null)
        {
            return Results.NotFound();
        }

        string documentUrl;
        try
        {
            await using var stream = file.OpenReadStream();

            documentUrl = await mediaStorageClient.UploadAsync(
                stream,
                file.FileName,
                file.ContentType,
                mapping.Category,
                cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { message = ex.Message });
        }

        var document = registration.Documents.FirstOrDefault(d =>
            NormalizeDocumentTypeKey(d.DocumentType ?? string.Empty) == key);

        if (document is null)
        {
            document = new RegistrationDocumentEntity { DocumentType = mapping.CanonicalType };
            registration.Documents.Add(document);
        }

        document.DocumentUrl = documentUrl;

        var updated = await repository.UpdateAsync(
            id,
            registration,
            cancellationToken);

        return updated
            ? Results.Ok(VisitorRegistrationResponse.FromEntity(registration))
            : Results.NotFound();
    }

    private static string NormalizeDocumentTypeKey(string documentType)
    {
        return new string(documentType.Where(char.IsLetterOrDigit).ToArray()).ToLowerInvariant();
    }

    private static string GenerateSixDigitCode()
    {
        return Random.Shared.Next(100000, 1000000).ToString(CultureInfo.InvariantCulture);
    }
}
