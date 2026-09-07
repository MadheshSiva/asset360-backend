using A360.Asset.Repository.Repositories;
using A360.Domain.Entities;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Activity;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class ModuleAccessMasterEndpoints
{
    private const string SequenceName = "module_access_master";
    private const string ModuleAccessMasterIdPrefix = "MAM";

    public static RouteGroupBuilder MapModuleAccessMasterEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/module-access-masters").WithTags("ModuleAccessMasters");

        group.MapGet("", GetModuleAccessMastersAsync).WithName("GetModuleAccessMasters");
        group.MapGet("/{id}", GetModuleAccessMasterByIdAsync).WithName("GetModuleAccessMasterById");
        group.MapPost("", CreateModuleAccessMasterAsync).WithName("CreateModuleAccessMaster");
        group.MapPut("/{id}", UpdateModuleAccessMasterAsync).WithName("UpdateModuleAccessMaster");
        group.MapDelete("/{id}", DeleteModuleAccessMasterAsync).WithName("DeleteModuleAccessMaster");

        return group;
    }

    private static async Task<IResult> GetModuleAccessMastersAsync(
        IModuleAccessMasterRepository repository,
        CancellationToken cancellationToken)
    {
        var moduleAccessMasters = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(moduleAccessMasters.Select(ModuleAccessMasterResponse.FromEntity));
    }

    private static async Task<IResult> GetModuleAccessMasterByIdAsync(
        string id,
        IModuleAccessMasterRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid module access master id." });
        }

        var moduleAccessMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (moduleAccessMaster is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("ModuleAccessMaster", moduleAccessMaster.ModuleId, moduleAccessMaster.ModuleName, EventAction.Viewed, cancellationToken);

        return Results.Ok(ModuleAccessMasterResponse.FromEntity(moduleAccessMaster));
    }

    private static async Task<IResult> CreateModuleAccessMasterAsync(
        CreateModuleAccessMasterRequest request,
        IModuleAccessMasterRepository repository,
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
        var moduleId = $"{ModuleAccessMasterIdPrefix}{nextSequence:D6}";

        var moduleAccessMaster = await repository.CreateAsync(
            request.ToEntity(moduleId, asset.AssetName),
            cancellationToken);

        await eventLogger.LogAsync("ModuleAccessMaster", moduleAccessMaster.ModuleId, moduleAccessMaster.ModuleName, EventAction.Created, cancellationToken);

        return Results.Created($"/api/module-access-masters/{moduleAccessMaster.Id}", ModuleAccessMasterResponse.FromEntity(moduleAccessMaster));
    }

    private static async Task<IResult> UpdateModuleAccessMasterAsync(
        string id,
        UpdateModuleAccessMasterRequest request,
        IModuleAccessMasterRepository repository,
        IAssetRepository assetRepository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid module access master id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var moduleAccessMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (moduleAccessMaster is null)
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

        request.ApplyTo(moduleAccessMaster, asset.AssetName);

        var updated = await repository.UpdateAsync(id, moduleAccessMaster, cancellationToken);
        if (updated)
        {
            await eventLogger.LogAsync("ModuleAccessMaster", moduleAccessMaster.ModuleId, moduleAccessMaster.ModuleName, EventAction.Updated, cancellationToken);
        }

        return updated ? Results.Ok(ModuleAccessMasterResponse.FromEntity(moduleAccessMaster)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteModuleAccessMasterAsync(
        string id,
        IModuleAccessMasterRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid module access master id." });
        }

        var moduleAccessMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (moduleAccessMaster is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (deleted)
        {
            await eventLogger.LogAsync("ModuleAccessMaster", moduleAccessMaster.ModuleId, moduleAccessMaster.ModuleName, EventAction.Deleted, cancellationToken);
        }

        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
