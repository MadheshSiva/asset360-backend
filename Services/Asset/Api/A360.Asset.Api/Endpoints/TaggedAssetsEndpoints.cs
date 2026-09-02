using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.Asset.Api.Endpoints;

public static class TaggedAssetsEndpoints
{
    private const string SequenceName = "tagged-asset";
    private const string TaggedAssetIdPrefix = "TAG";

    public static RouteGroupBuilder MapTaggedAssetsEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/tagged-assets").WithTags("TaggedAssets");

        group.MapGet("", GetTaggedAssetsListAsync).WithName("GetTaggedAssetsList");
        group.MapGet("/{id}", GetTaggedAssetsByIdAsync).WithName("GetTaggedAssetsById");
        group.MapGet("/by-asset/{assetId}", GetTaggedAssetsByAssetIdAsync).WithName("GetTaggedAssetsByAssetId");
        group.MapPost("", CreateTaggedAssetsAsync).WithName("CreateTaggedAssets");
        group.MapPut("/{id}", UpdateTaggedAssetsAsync).WithName("UpdateTaggedAssets");
        group.MapDelete("/{id}", DeleteTaggedAssetsAsync).WithName("DeleteTaggedAssets");

        return group;
    }

    private static async Task<IResult> GetTaggedAssetsListAsync(
        ITaggedAssetsRepository repository,
        CancellationToken cancellationToken)
    {
        var taggedAssets = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(taggedAssets.Select(TaggedAssetsResponse.FromEntity));
    }

    private static async Task<IResult> GetTaggedAssetsByIdAsync(
        string id,
        ITaggedAssetsRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid tagged asset id." });
        }

        var taggedAsset = await repository.GetByIdAsync(id, cancellationToken);
        return taggedAsset is null ? Results.NotFound() : Results.Ok(TaggedAssetsResponse.FromEntity(taggedAsset));
    }

    private static async Task<IResult> GetTaggedAssetsByAssetIdAsync(
        string assetId,
        ITaggedAssetsRepository repository,
        CancellationToken cancellationToken)
    {
        var taggedAssets = await repository.GetByAssetIdAsync(assetId, cancellationToken);
        return Results.Ok(taggedAssets.Select(TaggedAssetsResponse.FromEntity));
    }

    private static async Task<IResult> CreateTaggedAssetsAsync(
        CreateTaggedAssetsRequest request,
        ITaggedAssetsRepository repository,
        ISequenceGenerator sequenceGenerator,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var taggedAssetId = $"{TaggedAssetIdPrefix}{nextSequence:D6}";

        var taggedAsset = await repository.CreateAsync(request.ToEntity(taggedAssetId), cancellationToken);
        return Results.Created($"/api/tagged-assets/{taggedAsset.Id}", TaggedAssetsResponse.FromEntity(taggedAsset));
    }

    private static async Task<IResult> UpdateTaggedAssetsAsync(
        string id,
        UpdateTaggedAssetsRequest request,
        ITaggedAssetsRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid tagged asset id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var taggedAsset = await repository.GetByIdAsync(id, cancellationToken);
        if (taggedAsset is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(taggedAsset);

        var updated = await repository.UpdateAsync(id, taggedAsset, cancellationToken);
        return updated ? Results.Ok(TaggedAssetsResponse.FromEntity(taggedAsset)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteTaggedAssetsAsync(
        string id,
        ITaggedAssetsRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid tagged asset id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
