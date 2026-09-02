using A360.Asset.Repository.Repositories;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class AssetTypeEndpoints
{
    private const string SequenceName = "asset_type";
    private const string AssetTypeIdPrefix = "ATY";

    public static RouteGroupBuilder MapAssetTypeEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/asset-types").WithTags("AssetTypes");

        group.MapGet("", GetAssetTypesAsync).WithName("GetAssetTypes");
        group.MapGet("/{id}", GetAssetTypeByIdAsync).WithName("GetAssetTypeById");
        group.MapPost("", CreateAssetTypeAsync).WithName("CreateAssetType");
        group.MapPut("/{id}", UpdateAssetTypeAsync).WithName("UpdateAssetType");
        group.MapDelete("/{id}", DeleteAssetTypeAsync).WithName("DeleteAssetType");

        return group;
    }

    private static async Task<IResult> GetAssetTypesAsync(
        IAssetTypeRepository repository,
        CancellationToken cancellationToken)
    {
        var assetTypes = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(assetTypes.Select(AssetTypeResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetTypeByIdAsync(
        string id,
        IAssetTypeRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset type id." });
        }

        var assetType = await repository.GetByIdAsync(id, cancellationToken);
        return assetType is null ? Results.NotFound() : Results.Ok(AssetTypeResponse.FromEntity(assetType));
    }

    private static async Task<IResult> CreateAssetTypeAsync(
        CreateAssetTypeRequest request,
        IAssetTypeRepository repository,
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
        var assetTypeId = $"{AssetTypeIdPrefix}{nextSequence:D6}";

        var assetType = await repository.CreateAsync(
            request.ToEntity(assetTypeId, asset.AssetName),
            cancellationToken);

        return Results.Created($"/api/asset-types/{assetType.Id}", AssetTypeResponse.FromEntity(assetType));
    }

    private static async Task<IResult> UpdateAssetTypeAsync(
        string id,
        UpdateAssetTypeRequest request,
        IAssetTypeRepository repository,
        IAssetRepository assetRepository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset type id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var assetType = await repository.GetByIdAsync(id, cancellationToken);
        if (assetType is null)
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

        request.ApplyTo(assetType, asset.AssetName);

        var updated = await repository.UpdateAsync(id, assetType, cancellationToken);
        return updated ? Results.Ok(AssetTypeResponse.FromEntity(assetType)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteAssetTypeAsync(
        string id,
        IAssetTypeRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset type id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
