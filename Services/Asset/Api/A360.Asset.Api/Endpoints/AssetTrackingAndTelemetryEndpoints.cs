using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;
using A360.Domain.Entities;
using A360.Repository.Activity;

namespace A360.Asset.Api.Endpoints;

public static class AssetTrackingAndTelemetryEndpoints
{
    private const string SequenceName = "asset-tracking-and-telemetry";
    private const string TrackingIdPrefix = "TRK";

    public static RouteGroupBuilder MapAssetTrackingAndTelemetryEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/asset-tracking-and-telemetry").WithTags("AssetTrackingAndTelemetry");

        group.MapGet("", GetAssetTrackingAndTelemetriesAsync).WithName("GetAssetTrackingAndTelemetries");
        group.MapGet("/{id}", GetAssetTrackingAndTelemetryByIdAsync).WithName("GetAssetTrackingAndTelemetryById");
        group.MapGet("/by-asset/{assetId}", GetAssetTrackingAndTelemetriesByAssetIdAsync).WithName("GetAssetTrackingAndTelemetriesByAssetId");
        group.MapPost("", CreateAssetTrackingAndTelemetryAsync).WithName("CreateAssetTrackingAndTelemetry");
        group.MapPut("/{id}", UpdateAssetTrackingAndTelemetryAsync).WithName("UpdateAssetTrackingAndTelemetry");
        group.MapDelete("/{id}", DeleteAssetTrackingAndTelemetryAsync).WithName("DeleteAssetTrackingAndTelemetry");

        return group;
    }

    private static async Task<IResult> GetAssetTrackingAndTelemetriesAsync(
        IAssetTrackingAndTelemetryRepository repository,
        CancellationToken cancellationToken)
    {
        var telemetries = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(telemetries.Select(AssetTrackingAndTelemetryResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetTrackingAndTelemetryByIdAsync(
        string id,
        IAssetTrackingAndTelemetryRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset tracking and telemetry id." });
        }

        var telemetry = await repository.GetByIdAsync(id, cancellationToken);
        if (telemetry is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetTrackingAndTelemetry", telemetry.TrackingId, telemetry.AssetName, EventAction.Viewed, cancellationToken);
        return Results.Ok(AssetTrackingAndTelemetryResponse.FromEntity(telemetry));
    }

    private static async Task<IResult> GetAssetTrackingAndTelemetriesByAssetIdAsync(
        string assetId,
        IAssetTrackingAndTelemetryRepository repository,
        CancellationToken cancellationToken)
    {
        var telemetries = await repository.GetByAssetIdAsync(assetId, cancellationToken);
        return Results.Ok(telemetries.Select(AssetTrackingAndTelemetryResponse.FromEntity));
    }

    private static async Task<IResult> CreateAssetTrackingAndTelemetryAsync(
        CreateAssetTrackingAndTelemetryRequest request,
        IAssetTrackingAndTelemetryRepository repository,
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
        var trackingId = $"{TrackingIdPrefix}{nextSequence:D6}";

        var telemetry = await repository.CreateAsync(request.ToEntity(trackingId), cancellationToken);
        await eventLogger.LogAsync("AssetTrackingAndTelemetry", telemetry.TrackingId, telemetry.AssetName, EventAction.Created, cancellationToken);
        return Results.Created($"/api/asset-tracking-and-telemetry/{telemetry.Id}", AssetTrackingAndTelemetryResponse.FromEntity(telemetry));
    }

    private static async Task<IResult> UpdateAssetTrackingAndTelemetryAsync(
        string id,
        UpdateAssetTrackingAndTelemetryRequest request,
        IAssetTrackingAndTelemetryRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset tracking and telemetry id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var telemetry = await repository.GetByIdAsync(id, cancellationToken);
        if (telemetry is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(telemetry);

        var updated = await repository.UpdateAsync(id, telemetry, cancellationToken);
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetTrackingAndTelemetry", telemetry.TrackingId, telemetry.AssetName, EventAction.Updated, cancellationToken);
        return Results.Ok(AssetTrackingAndTelemetryResponse.FromEntity(telemetry));
    }

    private static async Task<IResult> DeleteAssetTrackingAndTelemetryAsync(
        string id,
        IAssetTrackingAndTelemetryRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset tracking and telemetry id." });
        }

        var telemetry = await repository.GetByIdAsync(id, cancellationToken);
        if (telemetry is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetTrackingAndTelemetry", telemetry.TrackingId, telemetry.AssetName, EventAction.Deleted, cancellationToken);
        return Results.NoContent();
    }
}
