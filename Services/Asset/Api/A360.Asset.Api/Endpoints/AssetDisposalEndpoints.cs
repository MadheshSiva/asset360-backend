using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.Asset.Api.Endpoints;

public static class AssetDisposalEndpoints
{
    private const string SequenceName = "asset-disposal";
    private const string DisposalIdPrefix = "DIS";

    public static RouteGroupBuilder MapAssetDisposalEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/asset-disposals").WithTags("AssetDisposals");

        group.MapGet("", GetAssetDisposalsAsync).WithName("GetAssetDisposals");
        group.MapGet("/{id}", GetAssetDisposalByIdAsync).WithName("GetAssetDisposalById");
        group.MapGet("/by-asset/{assetId}", GetAssetDisposalsByAssetIdAsync).WithName("GetAssetDisposalsByAssetId");
        group.MapPost("", CreateAssetDisposalAsync).WithName("CreateAssetDisposal");
        group.MapPut("/{id}", UpdateAssetDisposalAsync).WithName("UpdateAssetDisposal");
        group.MapDelete("/{id}", DeleteAssetDisposalAsync).WithName("DeleteAssetDisposal");

        return group;
    }

    private static async Task<IResult> GetAssetDisposalsAsync(
        IAssetDisposalRepository repository,
        CancellationToken cancellationToken)
    {
        var disposals = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(disposals.Select(AssetDisposalResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetDisposalByIdAsync(
        string id,
        IAssetDisposalRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset disposal id." });
        }

        var disposal = await repository.GetByIdAsync(id, cancellationToken);
        return disposal is null ? Results.NotFound() : Results.Ok(AssetDisposalResponse.FromEntity(disposal));
    }

    private static async Task<IResult> GetAssetDisposalsByAssetIdAsync(
        string assetId,
        IAssetDisposalRepository repository,
        CancellationToken cancellationToken)
    {
        var disposals = await repository.GetByAssetIdAsync(assetId, cancellationToken);
        return Results.Ok(disposals.Select(AssetDisposalResponse.FromEntity));
    }

    private static async Task<IResult> CreateAssetDisposalAsync(
        CreateAssetDisposalRequest request,
        IAssetDisposalRepository repository,
        ISequenceGenerator sequenceGenerator,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var disposalId = $"{DisposalIdPrefix}{nextSequence:D6}";

        var disposal = await repository.CreateAsync(request.ToEntity(disposalId), cancellationToken);
        return Results.Created($"/api/asset-disposals/{disposal.Id}", AssetDisposalResponse.FromEntity(disposal));
    }

    private static async Task<IResult> UpdateAssetDisposalAsync(
        string id,
        UpdateAssetDisposalRequest request,
        IAssetDisposalRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset disposal id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var disposal = await repository.GetByIdAsync(id, cancellationToken);
        if (disposal is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(disposal);

        var updated = await repository.UpdateAsync(id, disposal, cancellationToken);
        return updated ? Results.Ok(AssetDisposalResponse.FromEntity(disposal)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteAssetDisposalAsync(
        string id,
        IAssetDisposalRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset disposal id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
