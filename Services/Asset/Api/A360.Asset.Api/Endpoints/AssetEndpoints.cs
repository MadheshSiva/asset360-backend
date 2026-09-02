using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.Asset.Api.Endpoints;

public static class AssetEndpoints
{
    private const string SequenceName = "asset";
    private const string AssetIdPrefix = "AST";

    public static RouteGroupBuilder MapAssetEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/assets").WithTags("Assets");

        group.MapGet("", GetAssetsAsync).WithName("GetAssets");
        group.MapGet("/{id}", GetAssetByIdAsync).WithName("GetAssetById");
        group.MapPost("", CreateAssetAsync).WithName("CreateAsset");
        group.MapPut("/{id}", UpdateAssetAsync).WithName("UpdateAsset");
        group.MapDelete("/{id}", DeleteAssetAsync).WithName("DeleteAsset");

        return group;
    }

    private static async Task<IResult> GetAssetsAsync(
        IAssetRepository repository,
        CancellationToken cancellationToken)
    {
        var assets = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(assets.Select(AssetResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetByIdAsync(
        string id,
        IAssetRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset id." });
        }

        var asset = await repository.GetByIdAsync(id, cancellationToken);
        return asset is null ? Results.NotFound() : Results.Ok(AssetResponse.FromEntity(asset));
    }

    private static async Task<IResult> CreateAssetAsync(
        CreateAssetRequest request,
        IAssetRepository repository,
        ISequenceGenerator sequenceGenerator,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var assetId = $"{AssetIdPrefix}{nextSequence:D6}";

        var asset = await repository.CreateAsync(request.ToEntity(assetId), cancellationToken);
        return Results.Created($"/api/assets/{asset.Id}", AssetResponse.FromEntity(asset));
    }

    private static async Task<IResult> UpdateAssetAsync(
        string id,
        UpdateAssetRequest request,
        IAssetRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var asset = await repository.GetByIdAsync(id, cancellationToken);
        if (asset is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(asset);

        var updated = await repository.UpdateAsync(id, asset, cancellationToken);
        return updated ? Results.Ok(AssetResponse.FromEntity(asset)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteAssetAsync(
        string id,
        IAssetRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
