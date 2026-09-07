using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;
using A360.Domain.Entities;
using A360.Repository.Activity;

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
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset activity id." });
        }

        var activity = await repository.GetByIdAsync(id, cancellationToken);
        if (activity is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetActivity", activity.ActivityId, activity.AssetName, EventAction.Viewed, cancellationToken);
        return Results.Ok(AssetActivityResponse.FromEntity(activity));
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
        IEventLogger eventLogger,
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
        await eventLogger.LogAsync("AssetActivity", activity.ActivityId, activity.AssetName, EventAction.Created, cancellationToken);
        return Results.Created($"/api/asset-activities/{activity.Id}", AssetActivityResponse.FromEntity(activity));
    }

    private static async Task<IResult> UpdateAssetActivityAsync(
        string id,
        UpdateAssetActivityRequest request,
        IAssetActivityRepository repository,
        IEventLogger eventLogger,
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
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetActivity", activity.ActivityId, activity.AssetName, EventAction.Updated, cancellationToken);
        return Results.Ok(AssetActivityResponse.FromEntity(activity));
    }

    private static async Task<IResult> DeleteAssetActivityAsync(
        string id,
        IAssetActivityRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset activity id." });
        }

        var activity = await repository.GetByIdAsync(id, cancellationToken);
        if (activity is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetActivity", activity.ActivityId, activity.AssetName, EventAction.Deleted, cancellationToken);
        return Results.NoContent();
    }
}
