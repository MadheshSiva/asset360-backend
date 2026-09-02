using A360.Asset.Repository.Repositories;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class UnitMasterEndpoints
{
    private const string SequenceName = "unit_master";
    private const string UnitIdPrefix = "UNM";

    public static RouteGroupBuilder MapUnitMasterEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/unit-masters").WithTags("UnitMasters");

        group.MapGet("", GetUnitMastersAsync).WithName("GetUnitMasters");
        group.MapGet("/{id}", GetUnitMasterByIdAsync).WithName("GetUnitMasterById");
        group.MapPost("", CreateUnitMasterAsync).WithName("CreateUnitMaster");
        group.MapPut("/{id}", UpdateUnitMasterAsync).WithName("UpdateUnitMaster");
        group.MapDelete("/{id}", DeleteUnitMasterAsync).WithName("DeleteUnitMaster");

        return group;
    }

    private static async Task<IResult> GetUnitMastersAsync(
        IUnitMasterRepository repository,
        CancellationToken cancellationToken)
    {
        var unitMasters = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(unitMasters.Select(UnitMasterResponse.FromEntity));
    }

    private static async Task<IResult> GetUnitMasterByIdAsync(
        string id,
        IUnitMasterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid unit id." });
        }

        var unitMaster = await repository.GetByIdAsync(id, cancellationToken);
        return unitMaster is null ? Results.NotFound() : Results.Ok(UnitMasterResponse.FromEntity(unitMaster));
    }

    private static async Task<IResult> CreateUnitMasterAsync(
        CreateUnitMasterRequest request,
        IUnitMasterRepository repository,
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
        var unitId = $"{UnitIdPrefix}{nextSequence:D6}";

        var unitMaster = await repository.CreateAsync(
            request.ToEntity(unitId, asset.AssetName),
            cancellationToken);

        return Results.Created($"/api/unit-masters/{unitMaster.Id}", UnitMasterResponse.FromEntity(unitMaster));
    }

    private static async Task<IResult> UpdateUnitMasterAsync(
        string id,
        UpdateUnitMasterRequest request,
        IUnitMasterRepository repository,
        IAssetRepository assetRepository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid unit id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var unitMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (unitMaster is null)
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

        request.ApplyTo(unitMaster, asset.AssetName);

        var updated = await repository.UpdateAsync(id, unitMaster, cancellationToken);
        return updated ? Results.Ok(UnitMasterResponse.FromEntity(unitMaster)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteUnitMasterAsync(
        string id,
        IUnitMasterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid unit id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
