using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.Asset.Api.Endpoints;

public static class AssetAuditEndpoints
{
    private const string SequenceName = "asset-audit";
    private const string AuditIdPrefix = "AUD";

    public static RouteGroupBuilder MapAssetAuditEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/asset-audits").WithTags("AssetAudits");

        group.MapGet("", GetAssetAuditsAsync).WithName("GetAssetAudits");
        group.MapGet("/{id}", GetAssetAuditByIdAsync).WithName("GetAssetAuditById");
        group.MapGet("/by-asset/{assetId}", GetAssetAuditsByAssetIdAsync).WithName("GetAssetAuditsByAssetId");
        group.MapPost("", CreateAssetAuditAsync).WithName("CreateAssetAudit");
        group.MapPut("/{id}", UpdateAssetAuditAsync).WithName("UpdateAssetAudit");
        group.MapDelete("/{id}", DeleteAssetAuditAsync).WithName("DeleteAssetAudit");

        return group;
    }

    private static async Task<IResult> GetAssetAuditsAsync(
        IAssetAuditRepository repository,
        CancellationToken cancellationToken)
    {
        var audits = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(audits.Select(AssetAuditResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetAuditByIdAsync(
        string id,
        IAssetAuditRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset audit id." });
        }

        var audit = await repository.GetByIdAsync(id, cancellationToken);
        return audit is null ? Results.NotFound() : Results.Ok(AssetAuditResponse.FromEntity(audit));
    }

    private static async Task<IResult> GetAssetAuditsByAssetIdAsync(
        string assetId,
        IAssetAuditRepository repository,
        CancellationToken cancellationToken)
    {
        var audits = await repository.GetByAssetIdAsync(assetId, cancellationToken);
        return Results.Ok(audits.Select(AssetAuditResponse.FromEntity));
    }

    private static async Task<IResult> CreateAssetAuditAsync(
        CreateAssetAuditRequest request,
        IAssetAuditRepository repository,
        ISequenceGenerator sequenceGenerator,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var auditId = $"{AuditIdPrefix}{nextSequence:D6}";

        var audit = await repository.CreateAsync(request.ToEntity(auditId), cancellationToken);
        return Results.Created($"/api/asset-audits/{audit.Id}", AssetAuditResponse.FromEntity(audit));
    }

    private static async Task<IResult> UpdateAssetAuditAsync(
        string id,
        UpdateAssetAuditRequest request,
        IAssetAuditRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset audit id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var audit = await repository.GetByIdAsync(id, cancellationToken);
        if (audit is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(audit);

        var updated = await repository.UpdateAsync(id, audit, cancellationToken);
        return updated ? Results.Ok(AssetAuditResponse.FromEntity(audit)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteAssetAuditAsync(
        string id,
        IAssetAuditRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset audit id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
