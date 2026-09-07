using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;
using A360.Domain.Entities;
using A360.Repository.Activity;

namespace A360.Asset.Api.Endpoints;

public static class AssetDocumentsEndpoints
{
    private const string SequenceName = "asset-document";
    private const string DocumentIdPrefix = "DOC";

    public static RouteGroupBuilder MapAssetDocumentsEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/asset-documents").WithTags("AssetDocuments");

        group.MapGet("", GetAssetDocumentsListAsync).WithName("GetAssetDocumentsList");
        group.MapGet("/{id}", GetAssetDocumentsByIdAsync).WithName("GetAssetDocumentsById");
        group.MapGet("/by-asset/{assetId}", GetAssetDocumentsByAssetIdAsync).WithName("GetAssetDocumentsByAssetId");
        group.MapPost("", CreateAssetDocumentsAsync).WithName("CreateAssetDocuments");
        group.MapPut("/{id}", UpdateAssetDocumentsAsync).WithName("UpdateAssetDocuments");
        group.MapDelete("/{id}", DeleteAssetDocumentsAsync).WithName("DeleteAssetDocuments");

        return group;
    }

    private static async Task<IResult> GetAssetDocumentsListAsync(
        IAssetDocumentsRepository repository,
        CancellationToken cancellationToken)
    {
        var documents = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(documents.Select(AssetDocumentsResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetDocumentsByIdAsync(
        string id,
        IAssetDocumentsRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset documents id." });
        }

        var document = await repository.GetByIdAsync(id, cancellationToken);
        if (document is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetDocuments", document.DocumentId, document.AssetName, EventAction.Viewed, cancellationToken);
        return Results.Ok(AssetDocumentsResponse.FromEntity(document));
    }

    private static async Task<IResult> GetAssetDocumentsByAssetIdAsync(
        string assetId,
        IAssetDocumentsRepository repository,
        CancellationToken cancellationToken)
    {
        var documents = await repository.GetByAssetIdAsync(assetId, cancellationToken);
        return Results.Ok(documents.Select(AssetDocumentsResponse.FromEntity));
    }

    private static async Task<IResult> CreateAssetDocumentsAsync(
        CreateAssetDocumentsRequest request,
        IAssetDocumentsRepository repository,
        ISequenceGenerator sequenceGenerator,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var documentId = $"{DocumentIdPrefix}{nextSequence:D6}";

        var document = await repository.CreateAsync(request.ToEntity(documentId), cancellationToken);
        await eventLogger.LogAsync("AssetDocuments", document.DocumentId, document.AssetName, EventAction.Created, cancellationToken);
        return Results.Created($"/api/asset-documents/{document.Id}", AssetDocumentsResponse.FromEntity(document));
    }

    private static async Task<IResult> UpdateAssetDocumentsAsync(
        string id,
        UpdateAssetDocumentsRequest request,
        IAssetDocumentsRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset documents id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var document = await repository.GetByIdAsync(id, cancellationToken);
        if (document is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(document);

        var updated = await repository.UpdateAsync(id, document, cancellationToken);
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetDocuments", document.DocumentId, document.AssetName, EventAction.Updated, cancellationToken);
        return Results.Ok(AssetDocumentsResponse.FromEntity(document));
    }

    private static async Task<IResult> DeleteAssetDocumentsAsync(
        string id,
        IAssetDocumentsRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset documents id." });
        }

        var document = await repository.GetByIdAsync(id, cancellationToken);
        if (document is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetDocuments", document.DocumentId, document.AssetName, EventAction.Deleted, cancellationToken);
        return Results.NoContent();
    }
}
