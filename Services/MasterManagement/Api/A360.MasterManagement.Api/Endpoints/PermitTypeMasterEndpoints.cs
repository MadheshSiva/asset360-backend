using A360.Asset.Repository.Repositories;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
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
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid permit type id." });
        }

        var permitTypeMaster = await repository.GetByIdAsync(id, cancellationToken);
        return permitTypeMaster is null ? Results.NotFound() : Results.Ok(PermitTypeMasterResponse.FromEntity(permitTypeMaster));
    }

    private static async Task<IResult> CreatePermitTypeMasterAsync(
        CreatePermitTypeMasterRequest request,
        IPermitTypeMasterRepository repository,
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
        var permitTypeId = $"{PermitTypeIdPrefix}{nextSequence:D6}";

        var permitTypeMaster = await repository.CreateAsync(
            request.ToEntity(permitTypeId, asset.AssetName),
            cancellationToken);

        return Results.Created($"/api/permit-type-masters/{permitTypeMaster.Id}", PermitTypeMasterResponse.FromEntity(permitTypeMaster));
    }

    private static async Task<IResult> UpdatePermitTypeMasterAsync(
        string id,
        UpdatePermitTypeMasterRequest request,
        IPermitTypeMasterRepository repository,
        IAssetRepository assetRepository,
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
        return updated ? Results.Ok(PermitTypeMasterResponse.FromEntity(permitTypeMaster)) : Results.NotFound();
    }

    private static async Task<IResult> DeletePermitTypeMasterAsync(
        string id,
        IPermitTypeMasterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid permit type id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
