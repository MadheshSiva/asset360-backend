using A360.Asset.Repository.Repositories;
using A360.Domain.Entities;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Activity;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class StatusMasterEndpoints
{
    private const string SequenceName = "status_master";
    private const string StatusIdPrefix = "STM";

    public static RouteGroupBuilder MapStatusMasterEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/status-masters").WithTags("StatusMasters");

        group.MapGet("", GetStatusMastersAsync).WithName("GetStatusMasters");
        group.MapGet("/{id}", GetStatusMasterByIdAsync).WithName("GetStatusMasterById");
        group.MapPost("", CreateStatusMasterAsync).WithName("CreateStatusMaster");
        group.MapPut("/{id}", UpdateStatusMasterAsync).WithName("UpdateStatusMaster");
        group.MapDelete("/{id}", DeleteStatusMasterAsync).WithName("DeleteStatusMaster");

        return group;
    }

    private static async Task<IResult> GetStatusMastersAsync(
        IStatusMasterRepository repository,
        CancellationToken cancellationToken)
    {
        var statusMasters = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(statusMasters.Select(StatusMasterResponse.FromEntity));
    }

    private static async Task<IResult> GetStatusMasterByIdAsync(
        string id,
        IStatusMasterRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid status master id." });
        }

        var statusMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (statusMaster is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("StatusMaster", statusMaster.StatusId, statusMaster.StatusName, EventAction.Viewed, cancellationToken);

        return Results.Ok(StatusMasterResponse.FromEntity(statusMaster));
    }

    private static async Task<IResult> CreateStatusMasterAsync(
        CreateStatusMasterRequest request,
        IStatusMasterRepository repository,
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

        var statusMaster = await repository.CreateAsync(
            request.ToEntity(statusId, asset.AssetName),
            cancellationToken);

        await eventLogger.LogAsync("StatusMaster", statusMaster.StatusId, statusMaster.StatusName, EventAction.Created, cancellationToken);

        return Results.Created($"/api/status-masters/{statusMaster.Id}", StatusMasterResponse.FromEntity(statusMaster));
    }

    private static async Task<IResult> UpdateStatusMasterAsync(
        string id,
        UpdateStatusMasterRequest request,
        IStatusMasterRepository repository,
        IAssetRepository assetRepository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid status master id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var statusMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (statusMaster is null)
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

        request.ApplyTo(statusMaster, asset.AssetName);

        var updated = await repository.UpdateAsync(id, statusMaster, cancellationToken);
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("StatusMaster", statusMaster.StatusId, statusMaster.StatusName, EventAction.Updated, cancellationToken);

        return Results.Ok(StatusMasterResponse.FromEntity(statusMaster));
    }

    private static async Task<IResult> DeleteStatusMasterAsync(
        string id,
        IStatusMasterRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid status master id." });
        }

        var statusMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (statusMaster is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("StatusMaster", statusMaster.StatusId, statusMaster.StatusName, EventAction.Deleted, cancellationToken);

        return Results.NoContent();
    }
}
