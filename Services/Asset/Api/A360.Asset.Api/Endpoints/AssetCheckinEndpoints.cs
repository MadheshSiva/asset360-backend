using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;
using A360.Domain.Entities;
using A360.Repository.Activity;

namespace A360.Asset.Api.Endpoints;

public static class AssetCheckinEndpoints
{
    private const string SequenceName = "asset-checkin";
    private const string CheckinIdPrefix = "CKI";

    public static RouteGroupBuilder MapAssetCheckinEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/asset-checkins").WithTags("AssetCheckins");

        group.MapGet("", GetAssetCheckinsAsync).WithName("GetAssetCheckins");
        group.MapGet("/{id}", GetAssetCheckinByIdAsync).WithName("GetAssetCheckinById");
        group.MapGet("/by-asset/{assetId}", GetAssetCheckinsByAssetIdAsync).WithName("GetAssetCheckinsByAssetId");
        group.MapPost("", CreateAssetCheckinAsync).WithName("CreateAssetCheckin");
        group.MapPut("/{id}", UpdateAssetCheckinAsync).WithName("UpdateAssetCheckin");
        group.MapDelete("/{id}", DeleteAssetCheckinAsync).WithName("DeleteAssetCheckin");

        return group;
    }

    private static async Task<IResult> GetAssetCheckinsAsync(
        IAssetCheckinRepository repository,
        CancellationToken cancellationToken)
    {
        var checkins = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(checkins.Select(AssetCheckinResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetCheckinByIdAsync(
        string id,
        IAssetCheckinRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset checkin id." });
        }

        var checkin = await repository.GetByIdAsync(id, cancellationToken);
        if (checkin is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetCheckin", checkin.CheckinId, checkin.AssetName, EventAction.Viewed, cancellationToken);
        return Results.Ok(AssetCheckinResponse.FromEntity(checkin));
    }

    private static async Task<IResult> GetAssetCheckinsByAssetIdAsync(
        string assetId,
        IAssetCheckinRepository repository,
        CancellationToken cancellationToken)
    {
        var checkins = await repository.GetByAssetIdAsync(assetId, cancellationToken);
        return Results.Ok(checkins.Select(AssetCheckinResponse.FromEntity));
    }

    private static async Task<IResult> CreateAssetCheckinAsync(
        CreateAssetCheckinRequest request,
        IAssetCheckinRepository repository,
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
        var checkinId = $"{CheckinIdPrefix}{nextSequence:D6}";

        var checkin = await repository.CreateAsync(request.ToEntity(checkinId), cancellationToken);
        await eventLogger.LogAsync("AssetCheckin", checkin.CheckinId, checkin.AssetName, EventAction.Created, cancellationToken);
        return Results.Created($"/api/asset-checkins/{checkin.Id}", AssetCheckinResponse.FromEntity(checkin));
    }

    private static async Task<IResult> UpdateAssetCheckinAsync(
        string id,
        UpdateAssetCheckinRequest request,
        IAssetCheckinRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset checkin id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var checkin = await repository.GetByIdAsync(id, cancellationToken);
        if (checkin is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(checkin);

        var updated = await repository.UpdateAsync(id, checkin, cancellationToken);
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetCheckin", checkin.CheckinId, checkin.AssetName, EventAction.Updated, cancellationToken);
        return Results.Ok(AssetCheckinResponse.FromEntity(checkin));
    }

    private static async Task<IResult> DeleteAssetCheckinAsync(
        string id,
        IAssetCheckinRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset checkin id." });
        }

        var checkin = await repository.GetByIdAsync(id, cancellationToken);
        if (checkin is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetCheckin", checkin.CheckinId, checkin.AssetName, EventAction.Deleted, cancellationToken);
        return Results.NoContent();
    }
}
