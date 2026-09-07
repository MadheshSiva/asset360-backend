using A360.Asset.Repository.Repositories;
using A360.Domain.Entities;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Activity;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class ConditionMasterEndpoints
{
    private const string SequenceName = "condition_master";
    private const string ConditionIdPrefix = "CDM";

    public static RouteGroupBuilder MapConditionMasterEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/condition-masters").WithTags("ConditionMasters");

        group.MapGet("", GetConditionMastersAsync).WithName("GetConditionMasters");
        group.MapGet("/{id}", GetConditionMasterByIdAsync).WithName("GetConditionMasterById");
        group.MapPost("", CreateConditionMasterAsync).WithName("CreateConditionMaster");
        group.MapPut("/{id}", UpdateConditionMasterAsync).WithName("UpdateConditionMaster");
        group.MapDelete("/{id}", DeleteConditionMasterAsync).WithName("DeleteConditionMaster");

        return group;
    }

    private static async Task<IResult> GetConditionMastersAsync(
        IConditionMasterRepository repository,
        CancellationToken cancellationToken)
    {
        var conditionMasters = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(conditionMasters.Select(ConditionMasterResponse.FromEntity));
    }

    private static async Task<IResult> GetConditionMasterByIdAsync(
        string id,
        IConditionMasterRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid condition master id." });
        }

        var conditionMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (conditionMaster is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("ConditionMaster", conditionMaster.ConditionId, conditionMaster.ConditionName, EventAction.Viewed, cancellationToken);

        return Results.Ok(ConditionMasterResponse.FromEntity(conditionMaster));
    }

    private static async Task<IResult> CreateConditionMasterAsync(
        CreateConditionMasterRequest request,
        IConditionMasterRepository repository,
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
        var conditionId = $"{ConditionIdPrefix}{nextSequence:D6}";

        var conditionMaster = await repository.CreateAsync(
            request.ToEntity(conditionId, asset.AssetName),
            cancellationToken);

        await eventLogger.LogAsync("ConditionMaster", conditionMaster.ConditionId, conditionMaster.ConditionName, EventAction.Created, cancellationToken);

        return Results.Created($"/api/condition-masters/{conditionMaster.Id}", ConditionMasterResponse.FromEntity(conditionMaster));
    }

    private static async Task<IResult> UpdateConditionMasterAsync(
        string id,
        UpdateConditionMasterRequest request,
        IConditionMasterRepository repository,
        IAssetRepository assetRepository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid condition master id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var conditionMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (conditionMaster is null)
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

        request.ApplyTo(conditionMaster, asset.AssetName);

        var updated = await repository.UpdateAsync(id, conditionMaster, cancellationToken);
        if (updated)
        {
            await eventLogger.LogAsync("ConditionMaster", conditionMaster.ConditionId, conditionMaster.ConditionName, EventAction.Updated, cancellationToken);
        }

        return updated ? Results.Ok(ConditionMasterResponse.FromEntity(conditionMaster)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteConditionMasterAsync(
        string id,
        IConditionMasterRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid condition master id." });
        }

        var conditionMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (conditionMaster is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (deleted)
        {
            await eventLogger.LogAsync("ConditionMaster", conditionMaster.ConditionId, conditionMaster.ConditionName, EventAction.Deleted, cancellationToken);
        }

        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
