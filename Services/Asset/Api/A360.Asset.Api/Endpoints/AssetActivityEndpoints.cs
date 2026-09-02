using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.Asset.Api.Endpoints;

public static class AssetActivityEndpoints
{
    private const string SequenceName = "asset-activity";
    private const string ActivityIdPrefix = "ACT";

    public static RouteGroupBuilder MapAssetActivityEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/asset-activities").WithTags("AssetActivities");

        group.MapGet("", GetAssetActivitiesAsync).WithName("GetAssetActivities");
        group.MapGet("/{id}", GetAssetActivityByIdAsync).WithName("GetAssetActivityById");
        group.MapGet("/by-asset/{assetId}", GetAssetActivitiesByAssetIdAsync).WithName("GetAssetActivitiesByAssetId");
        group.MapPost("", CreateAssetActivityAsync).WithName("CreateAssetActivity");
        group.MapPut("/{id}", UpdateAssetActivityAsync).WithName("UpdateAssetActivity");
        group.MapDelete("/{id}", DeleteAssetActivityAsync).WithName("DeleteAssetActivity");

        return group;
    }

    private static async Task<IResult> GetAssetActivitiesAsync(
        IAssetActivityRepository repository,
        CancellationToken cancellationToken)
    {
        var activities = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(activities.Select(AssetActivityResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetActivityByIdAsync(
        string id,
        IAssetActivityRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset activity id." });
        }

        var activity = await repository.GetByIdAsync(id, cancellationToken);
        return activity is null ? Results.NotFound() : Results.Ok(AssetActivityResponse.FromEntity(activity));
    }

    private static async Task<IResult> GetAssetActivitiesByAssetIdAsync(
        string assetId,
        IAssetActivityRepository repository,
        CancellationToken cancellationToken)
    {
        var activities = await repository.GetByAssetIdAsync(assetId, cancellationToken);
        return Results.Ok(activities.Select(AssetActivityResponse.FromEntity));
    }

    private static async Task<IResult> CreateAssetActivityAsync(
        CreateAssetActivityRequest request,
        IAssetActivityRepository repository,
        ISequenceGenerator sequenceGenerator,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var activityId = $"{ActivityIdPrefix}{nextSequence:D6}";

        var activity = await repository.CreateAsync(request.ToEntity(activityId), cancellationToken);
        return Results.Created($"/api/asset-activities/{activity.Id}", AssetActivityResponse.FromEntity(activity));
    }

    private static async Task<IResult> UpdateAssetActivityAsync(
        string id,
        UpdateAssetActivityRequest request,
        IAssetActivityRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset activity id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var activity = await repository.GetByIdAsync(id, cancellationToken);
        if (activity is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(activity);

        var updated = await repository.UpdateAsync(id, activity, cancellationToken);
        return updated ? Results.Ok(AssetActivityResponse.FromEntity(activity)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteAssetActivityAsync(
        string id,
        IAssetActivityRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset activity id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
