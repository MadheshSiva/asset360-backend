using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.Asset.Api.Endpoints;

public static class AssetAuditAndVerificationEndpoints
{
    private const string SequenceName = "asset-audit-and-verification";
    private const string AuditVerificationIdPrefix = "AAV";

    public static RouteGroupBuilder MapAssetAuditAndVerificationEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/asset-audit-and-verifications").WithTags("AssetAuditAndVerifications");

        group.MapGet("", GetAssetAuditAndVerificationsAsync).WithName("GetAssetAuditAndVerifications");
        group.MapGet("/{id}", GetAssetAuditAndVerificationByIdAsync).WithName("GetAssetAuditAndVerificationById");
        group.MapGet("/by-asset/{assetId}", GetAssetAuditAndVerificationsByAssetIdAsync).WithName("GetAssetAuditAndVerificationsByAssetId");
        group.MapPost("", CreateAssetAuditAndVerificationAsync).WithName("CreateAssetAuditAndVerification");
        group.MapPut("/{id}", UpdateAssetAuditAndVerificationAsync).WithName("UpdateAssetAuditAndVerification");
        group.MapDelete("/{id}", DeleteAssetAuditAndVerificationAsync).WithName("DeleteAssetAuditAndVerification");

        return group;
    }

    private static async Task<IResult> GetAssetAuditAndVerificationsAsync(
        IAssetAuditAndVerificationRepository repository,
        CancellationToken cancellationToken)
    {
        var records = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(records.Select(AssetAuditAndVerificationResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetAuditAndVerificationByIdAsync(
        string id,
        IAssetAuditAndVerificationRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset audit and verification id." });
        }

        var record = await repository.GetByIdAsync(id, cancellationToken);
        return record is null ? Results.NotFound() : Results.Ok(AssetAuditAndVerificationResponse.FromEntity(record));
    }

    private static async Task<IResult> GetAssetAuditAndVerificationsByAssetIdAsync(
        string assetId,
        IAssetAuditAndVerificationRepository repository,
        CancellationToken cancellationToken)
    {
        var records = await repository.GetByAssetIdAsync(assetId, cancellationToken);
        return Results.Ok(records.Select(AssetAuditAndVerificationResponse.FromEntity));
    }

    private static async Task<IResult> CreateAssetAuditAndVerificationAsync(
        CreateAssetAuditAndVerificationRequest request,
        IAssetAuditAndVerificationRepository repository,
        ISequenceGenerator sequenceGenerator,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var auditVerificationId = $"{AuditVerificationIdPrefix}{nextSequence:D6}";

        var record = await repository.CreateAsync(request.ToEntity(auditVerificationId), cancellationToken);
        return Results.Created($"/api/asset-audit-and-verifications/{record.Id}", AssetAuditAndVerificationResponse.FromEntity(record));
    }

    private static async Task<IResult> UpdateAssetAuditAndVerificationAsync(
        string id,
        UpdateAssetAuditAndVerificationRequest request,
        IAssetAuditAndVerificationRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset audit and verification id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var record = await repository.GetByIdAsync(id, cancellationToken);
        if (record is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(record);

        var updated = await repository.UpdateAsync(id, record, cancellationToken);
        return updated ? Results.Ok(AssetAuditAndVerificationResponse.FromEntity(record)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteAssetAuditAndVerificationAsync(
        string id,
        IAssetAuditAndVerificationRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset audit and verification id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
