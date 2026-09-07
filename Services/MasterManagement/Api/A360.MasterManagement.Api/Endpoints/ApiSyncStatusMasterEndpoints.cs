using A360.Asset.Repository.Repositories;
using A360.Domain.Entities;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Activity;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class ApiSyncStatusMasterEndpoints
{
    private const string SequenceName = "api_sync_status_master";
    private const string StatusIdPrefix = "ASM";

    public static RouteGroupBuilder MapApiSyncStatusMasterEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/api-sync-status-masters").WithTags("ApiSyncStatusMasters");

        group.MapGet("", GetApiSyncStatusMastersAsync).WithName("GetApiSyncStatusMasters");
        group.MapGet("/{id}", GetApiSyncStatusMasterByIdAsync).WithName("GetApiSyncStatusMasterById");
        group.MapPost("", CreateApiSyncStatusMasterAsync).WithName("CreateApiSyncStatusMaster");
        group.MapPut("/{id}", UpdateApiSyncStatusMasterAsync).WithName("UpdateApiSyncStatusMaster");
        group.MapDelete("/{id}", DeleteApiSyncStatusMasterAsync).WithName("DeleteApiSyncStatusMaster");

        return group;
    }

    private static async Task<IResult> GetApiSyncStatusMastersAsync(
        IApiSyncStatusMasterRepository repository,
        CancellationToken cancellationToken)
    {
        var apiSyncStatusMasters = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(apiSyncStatusMasters.Select(ApiSyncStatusMasterResponse.FromEntity));
    }

    private static async Task<IResult> GetApiSyncStatusMasterByIdAsync(
        string id,
        IApiSyncStatusMasterRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid api sync status master id." });
        }

        var apiSyncStatusMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (apiSyncStatusMaster is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("ApiSyncStatusMaster", apiSyncStatusMaster.StatusId, apiSyncStatusMaster.StatusName, EventAction.Viewed, cancellationToken);

        return Results.Ok(ApiSyncStatusMasterResponse.FromEntity(apiSyncStatusMaster));
    }

    private static async Task<IResult> CreateApiSyncStatusMasterAsync(
        CreateApiSyncStatusMasterRequest request,
        IApiSyncStatusMasterRepository repository,
        IAssetRepository assetRepository,
        ISequenceGenerator sequenceGenerator,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var asset = await assetRepository.GetByAssetIdAsync(request.AssetId!, cancellationToken);
        if (asset is null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["AssetId"] = ["No asset exists with this AssetId"]
            });
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var statusId = $"{StatusIdPrefix}{nextSequence:D6}";

        var apiSyncStatusMaster = await repository.CreateAsync(
            request.ToEntity(statusId, asset.AssetName),
            cancellationToken);

        await eventLogger.LogAsync("ApiSyncStatusMaster", apiSyncStatusMaster.StatusId, apiSyncStatusMaster.StatusName, EventAction.Created, cancellationToken);

        return Results.Created($"/api/api-sync-status-masters/{apiSyncStatusMaster.Id}", ApiSyncStatusMasterResponse.FromEntity(apiSyncStatusMaster));
    }

    private static async Task<IResult> UpdateApiSyncStatusMasterAsync(
        string id,
        UpdateApiSyncStatusMasterRequest request,
        IApiSyncStatusMasterRepository repository,
        IAssetRepository assetRepository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid api sync status master id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var apiSyncStatusMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (apiSyncStatusMaster is null)
        {
            return Results.NotFound();
        }

        var asset = await assetRepository.GetByAssetIdAsync(request.AssetId!, cancellationToken);
        if (asset is null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["AssetId"] = ["No asset exists with this AssetId"]
            });
        }

        request.ApplyTo(apiSyncStatusMaster, asset.AssetName);

        var updated = await repository.UpdateAsync(id, apiSyncStatusMaster, cancellationToken);
        if (updated)
        {
            await eventLogger.LogAsync("ApiSyncStatusMaster", apiSyncStatusMaster.StatusId, apiSyncStatusMaster.StatusName, EventAction.Updated, cancellationToken);
        }

        return updated ? Results.Ok(ApiSyncStatusMasterResponse.FromEntity(apiSyncStatusMaster)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteApiSyncStatusMasterAsync(
        string id,
        IApiSyncStatusMasterRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid api sync status master id." });
        }

        var apiSyncStatusMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (apiSyncStatusMaster is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (deleted)
        {
            await eventLogger.LogAsync("ApiSyncStatusMaster", apiSyncStatusMaster.StatusId, apiSyncStatusMaster.StatusName, EventAction.Deleted, cancellationToken);
        }

        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
