using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.Asset.Api.Endpoints;

public static class AssetLifecycleEndpoints
{
    private const string SequenceName = "asset-lifecycle";
    private const string LifecycleIdPrefix = "ALC";

    public static RouteGroupBuilder MapAssetLifecycleEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/asset-lifecycles").WithTags("AssetLifecycles");

        group.MapGet("", GetAssetLifecyclesAsync).WithName("GetAssetLifecycles");
        group.MapGet("/{id}", GetAssetLifecycleByIdAsync).WithName("GetAssetLifecycleById");
        group.MapGet("/by-asset/{assetId}", GetAssetLifecyclesByAssetIdAsync).WithName("GetAssetLifecyclesByAssetId");
        group.MapPost("", CreateAssetLifecycleAsync).WithName("CreateAssetLifecycle");
        group.MapPut("/{id}", UpdateAssetLifecycleAsync).WithName("UpdateAssetLifecycle");
        group.MapDelete("/{id}", DeleteAssetLifecycleAsync).WithName("DeleteAssetLifecycle");

        return group;
    }

    private static async Task<IResult> GetAssetLifecyclesAsync(
        IAssetLifecycleRepository repository,
        CancellationToken cancellationToken)
    {
        var lifecycles = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(lifecycles.Select(AssetLifecycleResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetLifecycleByIdAsync(
        string id,
        IAssetLifecycleRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset lifecycle id." });
        }

        var lifecycle = await repository.GetByIdAsync(id, cancellationToken);
        return lifecycle is null ? Results.NotFound() : Results.Ok(AssetLifecycleResponse.FromEntity(lifecycle));
    }

    private static async Task<IResult> GetAssetLifecyclesByAssetIdAsync(
        string assetId,
        IAssetLifecycleRepository repository,
        CancellationToken cancellationToken)
    {
        var lifecycles = await repository.GetByAssetIdAsync(assetId, cancellationToken);
        return Results.Ok(lifecycles.Select(AssetLifecycleResponse.FromEntity));
    }

    private static async Task<IResult> CreateAssetLifecycleAsync(
        CreateAssetLifecycleRequest request,
        IAssetLifecycleRepository repository,
        ISequenceGenerator sequenceGenerator,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var lifecycleId = $"{LifecycleIdPrefix}{nextSequence:D6}";

        var lifecycle = await repository.CreateAsync(request.ToEntity(lifecycleId), cancellationToken);
        return Results.Created($"/api/asset-lifecycles/{lifecycle.Id}", AssetLifecycleResponse.FromEntity(lifecycle));
    }

    private static async Task<IResult> UpdateAssetLifecycleAsync(
        string id,
        UpdateAssetLifecycleRequest request,
        IAssetLifecycleRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset lifecycle id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var lifecycle = await repository.GetByIdAsync(id, cancellationToken);
        if (lifecycle is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(lifecycle);

        var updated = await repository.UpdateAsync(id, lifecycle, cancellationToken);
        return updated ? Results.Ok(AssetLifecycleResponse.FromEntity(lifecycle)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteAssetLifecycleAsync(
        string id,
        IAssetLifecycleRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset lifecycle id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
