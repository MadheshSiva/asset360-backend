using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Domain.Entities;
using A360.Repository.Activity;

namespace A360.Asset.Api.Endpoints;

public static class AssetLocationEndpoints
{
    public static RouteGroupBuilder MapAssetLocationEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/asset-locations").WithTags("AssetLocations");

        group.MapGet("", GetAssetLocationsAsync).WithName("GetAssetLocations");
        group.MapGet("/{id}", GetAssetLocationByIdAsync).WithName("GetAssetLocationById");
        group.MapPost("", CreateAssetLocationAsync).WithName("CreateAssetLocation");
        group.MapPut("/{id}", UpdateAssetLocationAsync).WithName("UpdateAssetLocation");
        group.MapDelete("/{id}", DeleteAssetLocationAsync).WithName("DeleteAssetLocation");

        return group;
    }

    private static async Task<IResult> GetAssetLocationsAsync(
        IAssetLocationRepository repository,
        CancellationToken cancellationToken)
    {
        var locations = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(locations.Select(AssetLocationResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetLocationByIdAsync(
        string id,
        IAssetLocationRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset location id." });
        }

        var location = await repository.GetByIdAsync(id, cancellationToken);
        if (location is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetLocation", location.Id, location.AssetName, EventAction.Viewed, cancellationToken);
        return Results.Ok(AssetLocationResponse.FromEntity(location));
    }

    private static async Task<IResult> CreateAssetLocationAsync(
        CreateAssetLocationRequest request,
        IAssetLocationRepository repository,
        IAssetRepository assetRepository,
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
            return Results.BadRequest(new { message = $"Asset '{request.AssetId}' does not exist." });
        }

        var location = await repository.CreateAsync(request.ToEntity(), cancellationToken);
        await eventLogger.LogAsync("AssetLocation", location.Id, location.AssetName, EventAction.Created, cancellationToken);
        return Results.Created($"/api/asset-locations/{location.Id}", AssetLocationResponse.FromEntity(location));
    }

    private static async Task<IResult> UpdateAssetLocationAsync(
        string id,
        UpdateAssetLocationRequest request,
        IAssetLocationRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset location id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var location = await repository.GetByIdAsync(id, cancellationToken);
        if (location is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(location);

        var updated = await repository.UpdateAsync(id, location, cancellationToken);
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetLocation", location.Id, location.AssetName, EventAction.Updated, cancellationToken);
        return Results.Ok(AssetLocationResponse.FromEntity(location));
    }

    private static async Task<IResult> DeleteAssetLocationAsync(
        string id,
        IAssetLocationRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset location id." });
        }

        var location = await repository.GetByIdAsync(id, cancellationToken);
        if (location is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetLocation", location.Id, location.AssetName, EventAction.Deleted, cancellationToken);
        return Results.NoContent();
    }
}
