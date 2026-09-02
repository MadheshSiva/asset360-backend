using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.Asset.Api.Endpoints;

public static class AssetMovementEndpoints
{
    private const string SequenceName = "asset-movement";
    private const string MovementIdPrefix = "MOV";

    public static RouteGroupBuilder MapAssetMovementEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/asset-movements").WithTags("AssetMovements");

        group.MapGet("", GetAssetMovementsAsync).WithName("GetAssetMovements");
        group.MapGet("/{id}", GetAssetMovementByIdAsync).WithName("GetAssetMovementById");
        group.MapGet("/by-asset/{assetId}", GetAssetMovementsByAssetIdAsync).WithName("GetAssetMovementsByAssetId");
        group.MapPost("", CreateAssetMovementAsync).WithName("CreateAssetMovement");
        group.MapPut("/{id}", UpdateAssetMovementAsync).WithName("UpdateAssetMovement");
        group.MapDelete("/{id}", DeleteAssetMovementAsync).WithName("DeleteAssetMovement");

        return group;
    }

    private static async Task<IResult> GetAssetMovementsAsync(
        IAssetMovementRepository repository,
        CancellationToken cancellationToken)
    {
        var movements = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(movements.Select(AssetMovementResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetMovementByIdAsync(
        string id,
        IAssetMovementRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset movement id." });
        }

        var movement = await repository.GetByIdAsync(id, cancellationToken);
        return movement is null ? Results.NotFound() : Results.Ok(AssetMovementResponse.FromEntity(movement));
    }

    private static async Task<IResult> GetAssetMovementsByAssetIdAsync(
        string assetId,
        IAssetMovementRepository repository,
        CancellationToken cancellationToken)
    {
        var movements = await repository.GetByAssetIdAsync(assetId, cancellationToken);
        return Results.Ok(movements.Select(AssetMovementResponse.FromEntity));
    }

    private static async Task<IResult> CreateAssetMovementAsync(
        CreateAssetMovementRequest request,
        IAssetMovementRepository repository,
        ISequenceGenerator sequenceGenerator,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var movementId = $"{MovementIdPrefix}{nextSequence:D6}";

        var movement = await repository.CreateAsync(request.ToEntity(movementId), cancellationToken);
        return Results.Created($"/api/asset-movements/{movement.Id}", AssetMovementResponse.FromEntity(movement));
    }

    private static async Task<IResult> UpdateAssetMovementAsync(
        string id,
        UpdateAssetMovementRequest request,
        IAssetMovementRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset movement id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var movement = await repository.GetByIdAsync(id, cancellationToken);
        if (movement is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(movement);

        var updated = await repository.UpdateAsync(id, movement, cancellationToken);
        return updated ? Results.Ok(AssetMovementResponse.FromEntity(movement)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteAssetMovementAsync(
        string id,
        IAssetMovementRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset movement id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
