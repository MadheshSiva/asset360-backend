using A360.Asset.Repository.Repositories;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
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
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid module access master id." });
        }

        var moduleAccessMaster = await repository.GetByIdAsync(id, cancellationToken);
        return moduleAccessMaster is null ? Results.NotFound() : Results.Ok(ModuleAccessMasterResponse.FromEntity(moduleAccessMaster));
    }

    private static async Task<IResult> CreateModuleAccessMasterAsync(
        CreateModuleAccessMasterRequest request,
        IModuleAccessMasterRepository repository,
        IAssetRepository assetRepository,
        ISequenceGenerator sequenceGenerator,
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

        return Results.Created($"/api/module-access-masters/{moduleAccessMaster.Id}", ModuleAccessMasterResponse.FromEntity(moduleAccessMaster));
    }

    private static async Task<IResult> UpdateModuleAccessMasterAsync(
        string id,
        UpdateModuleAccessMasterRequest request,
        IModuleAccessMasterRepository repository,
        IAssetRepository assetRepository,
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
        return updated ? Results.Ok(ModuleAccessMasterResponse.FromEntity(moduleAccessMaster)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteModuleAccessMasterAsync(
        string id,
        IModuleAccessMasterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid module access master id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
