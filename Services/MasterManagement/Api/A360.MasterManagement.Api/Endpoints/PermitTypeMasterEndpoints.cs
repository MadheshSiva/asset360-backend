using A360.Asset.Repository.Repositories;
using A360.Domain.Entities;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Activity;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class PermitTypeMasterEndpoints
{
    private const string SequenceName = "permit_type_master";
    private const string PermitTypeIdPrefix = "PTM";

    public static RouteGroupBuilder MapPermitTypeMasterEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/permit-type-masters").WithTags("PermitTypeMasters");

        group.MapGet("", GetPermitTypeMastersAsync).WithName("GetPermitTypeMasters");
        group.MapGet("/{id}", GetPermitTypeMasterByIdAsync).WithName("GetPermitTypeMasterById");
        group.MapPost("", CreatePermitTypeMasterAsync).WithName("CreatePermitTypeMaster");
        group.MapPut("/{id}", UpdatePermitTypeMasterAsync).WithName("UpdatePermitTypeMaster");
        group.MapDelete("/{id}", DeletePermitTypeMasterAsync).WithName("DeletePermitTypeMaster");

        return group;
    }

    private static async Task<IResult> GetPermitTypeMastersAsync(
        IPermitTypeMasterRepository repository,
        CancellationToken cancellationToken)
    {
        var permitTypeMasters = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(permitTypeMasters.Select(PermitTypeMasterResponse.FromEntity));
    }

    private static async Task<IResult> GetPermitTypeMasterByIdAsync(
        string id,
        IPermitTypeMasterRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid permit type id." });
        }

        var permitTypeMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (permitTypeMaster is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("PermitTypeMaster", permitTypeMaster.PermitTypeId, permitTypeMaster.PermitName, EventAction.Viewed, cancellationToken);
        return Results.Ok(PermitTypeMasterResponse.FromEntity(permitTypeMaster));
    }

    private static async Task<IResult> CreatePermitTypeMasterAsync(
        CreatePermitTypeMasterRequest request,
        IPermitTypeMasterRepository repository,
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
        var permitTypeId = $"{PermitTypeIdPrefix}{nextSequence:D6}";

        var permitTypeMaster = await repository.CreateAsync(
            request.ToEntity(permitTypeId, asset.AssetName),
            cancellationToken);

        await eventLogger.LogAsync("PermitTypeMaster", permitTypeMaster.PermitTypeId, permitTypeMaster.PermitName, EventAction.Created, cancellationToken);

        return Results.Created($"/api/permit-type-masters/{permitTypeMaster.Id}", PermitTypeMasterResponse.FromEntity(permitTypeMaster));
    }

    private static async Task<IResult> UpdatePermitTypeMasterAsync(
        string id,
        UpdatePermitTypeMasterRequest request,
        IPermitTypeMasterRepository repository,
        IAssetRepository assetRepository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid permit type id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var permitTypeMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (permitTypeMaster is null)
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

        request.ApplyTo(permitTypeMaster, asset.AssetName);

        var updated = await repository.UpdateAsync(id, permitTypeMaster, cancellationToken);
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("PermitTypeMaster", permitTypeMaster.PermitTypeId, permitTypeMaster.PermitName, EventAction.Updated, cancellationToken);
        return Results.Ok(PermitTypeMasterResponse.FromEntity(permitTypeMaster));
    }

    private static async Task<IResult> DeletePermitTypeMasterAsync(
        string id,
        IPermitTypeMasterRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid permit type id." });
        }

        var permitTypeMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (permitTypeMaster is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("PermitTypeMaster", permitTypeMaster.PermitTypeId, permitTypeMaster.PermitName, EventAction.Deleted, cancellationToken);
        return Results.NoContent();
    }
}
