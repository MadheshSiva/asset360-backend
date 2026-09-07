using A360.Asset.Repository.Repositories;
using A360.Domain.Entities;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Activity;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class PermissionMasterEndpoints
{
    private const string SequenceName = "permission_master";
    private const string PermissionMasterIdPrefix = "PRM";

    public static RouteGroupBuilder MapPermissionMasterEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/permission-masters").WithTags("PermissionMasters");

        group.MapGet("", GetPermissionMastersAsync).WithName("GetPermissionMasters");
        group.MapGet("/{id}", GetPermissionMasterByIdAsync).WithName("GetPermissionMasterById");
        group.MapPost("", CreatePermissionMasterAsync).WithName("CreatePermissionMaster");
        group.MapPut("/{id}", UpdatePermissionMasterAsync).WithName("UpdatePermissionMaster");
        group.MapDelete("/{id}", DeletePermissionMasterAsync).WithName("DeletePermissionMaster");

        return group;
    }

    private static async Task<IResult> GetPermissionMastersAsync(
        IPermissionMasterRepository repository,
        CancellationToken cancellationToken)
    {
        var permissionMasters = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(permissionMasters.Select(PermissionMasterResponse.FromEntity));
    }

    private static async Task<IResult> GetPermissionMasterByIdAsync(
        string id,
        IPermissionMasterRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid permission master id." });
        }

        var permissionMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (permissionMaster is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("PermissionMaster", permissionMaster.PermissionId, permissionMaster.PermissionName, EventAction.Viewed, cancellationToken);
        return Results.Ok(PermissionMasterResponse.FromEntity(permissionMaster));
    }

    private static async Task<IResult> CreatePermissionMasterAsync(
        CreatePermissionMasterRequest request,
        IPermissionMasterRepository repository,
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
        var permissionId = $"{PermissionMasterIdPrefix}{nextSequence:D6}";

        var permissionMaster = await repository.CreateAsync(
            request.ToEntity(permissionId, asset.AssetName),
            cancellationToken);

        await eventLogger.LogAsync("PermissionMaster", permissionMaster.PermissionId, permissionMaster.PermissionName, EventAction.Created, cancellationToken);

        return Results.Created($"/api/permission-masters/{permissionMaster.Id}", PermissionMasterResponse.FromEntity(permissionMaster));
    }

    private static async Task<IResult> UpdatePermissionMasterAsync(
        string id,
        UpdatePermissionMasterRequest request,
        IPermissionMasterRepository repository,
        IAssetRepository assetRepository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid permission master id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var permissionMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (permissionMaster is null)
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

        request.ApplyTo(permissionMaster, asset.AssetName);

        var updated = await repository.UpdateAsync(id, permissionMaster, cancellationToken);
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("PermissionMaster", permissionMaster.PermissionId, permissionMaster.PermissionName, EventAction.Updated, cancellationToken);
        return Results.Ok(PermissionMasterResponse.FromEntity(permissionMaster));
    }

    private static async Task<IResult> DeletePermissionMasterAsync(
        string id,
        IPermissionMasterRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid permission master id." });
        }

        var permissionMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (permissionMaster is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("PermissionMaster", permissionMaster.PermissionId, permissionMaster.PermissionName, EventAction.Deleted, cancellationToken);
        return Results.NoContent();
    }
}
