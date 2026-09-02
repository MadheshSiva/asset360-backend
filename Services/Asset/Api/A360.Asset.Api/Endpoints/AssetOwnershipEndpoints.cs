using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.Asset.Api.Endpoints;

public static class AssetOwnershipEndpoints
{
    private const string SequenceName = "asset-ownership";
    private const string OwnershipIdPrefix = "OWN";

    public static RouteGroupBuilder MapAssetOwnershipEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/asset-ownerships").WithTags("AssetOwnerships");

        group.MapGet("", GetAssetOwnershipsAsync).WithName("GetAssetOwnerships");
        group.MapGet("/{id}", GetAssetOwnershipByIdAsync).WithName("GetAssetOwnershipById");
        group.MapGet("/by-asset/{assetId}", GetAssetOwnershipsByAssetIdAsync).WithName("GetAssetOwnershipsByAssetId");
        group.MapPost("", CreateAssetOwnershipAsync).WithName("CreateAssetOwnership");
        group.MapPut("/{id}", UpdateAssetOwnershipAsync).WithName("UpdateAssetOwnership");
        group.MapDelete("/{id}", DeleteAssetOwnershipAsync).WithName("DeleteAssetOwnership");

        return group;
    }

    private static async Task<IResult> GetAssetOwnershipsAsync(
        IAssetOwnershipRepository repository,
        CancellationToken cancellationToken)
    {
        var ownerships = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(ownerships.Select(AssetOwnershipResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetOwnershipByIdAsync(
        string id,
        IAssetOwnershipRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset ownership id." });
        }

        var ownership = await repository.GetByIdAsync(id, cancellationToken);
        return ownership is null ? Results.NotFound() : Results.Ok(AssetOwnershipResponse.FromEntity(ownership));
    }

    private static async Task<IResult> GetAssetOwnershipsByAssetIdAsync(
        string assetId,
        IAssetOwnershipRepository repository,
        CancellationToken cancellationToken)
    {
        var ownerships = await repository.GetByAssetIdAsync(assetId, cancellationToken);
        return Results.Ok(ownerships.Select(AssetOwnershipResponse.FromEntity));
    }

    private static async Task<IResult> CreateAssetOwnershipAsync(
        CreateAssetOwnershipRequest request,
        IAssetOwnershipRepository repository,
        ISequenceGenerator sequenceGenerator,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var ownershipId = $"{OwnershipIdPrefix}{nextSequence:D6}";

        var ownership = await repository.CreateAsync(request.ToEntity(ownershipId), cancellationToken);
        return Results.Created($"/api/asset-ownerships/{ownership.Id}", AssetOwnershipResponse.FromEntity(ownership));
    }

    private static async Task<IResult> UpdateAssetOwnershipAsync(
        string id,
        UpdateAssetOwnershipRequest request,
        IAssetOwnershipRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset ownership id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var ownership = await repository.GetByIdAsync(id, cancellationToken);
        if (ownership is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(ownership);

        var updated = await repository.UpdateAsync(id, ownership, cancellationToken);
        return updated ? Results.Ok(AssetOwnershipResponse.FromEntity(ownership)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteAssetOwnershipAsync(
        string id,
        IAssetOwnershipRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset ownership id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
