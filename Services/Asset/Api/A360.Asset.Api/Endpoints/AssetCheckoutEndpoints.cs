using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;
using A360.Domain.Entities;
using A360.Repository.Activity;

namespace A360.Asset.Api.Endpoints;

public static class AssetCheckoutEndpoints
{
    private const string SequenceName = "asset-checkout";
    private const string CheckoutIdPrefix = "CKO";

    public static RouteGroupBuilder MapAssetCheckoutEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/asset-checkouts").WithTags("AssetCheckouts");

        group.MapGet("", GetAssetCheckoutsAsync).WithName("GetAssetCheckouts");
        group.MapGet("/{id}", GetAssetCheckoutByIdAsync).WithName("GetAssetCheckoutById");
        group.MapGet("/by-asset/{assetId}", GetAssetCheckoutsByAssetIdAsync).WithName("GetAssetCheckoutsByAssetId");
        group.MapPost("", CreateAssetCheckoutAsync).WithName("CreateAssetCheckout");
        group.MapPut("/{id}", UpdateAssetCheckoutAsync).WithName("UpdateAssetCheckout");
        group.MapDelete("/{id}", DeleteAssetCheckoutAsync).WithName("DeleteAssetCheckout");

        return group;
    }

    private static async Task<IResult> GetAssetCheckoutsAsync(
        IAssetCheckoutRepository repository,
        CancellationToken cancellationToken)
    {
        var checkouts = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(checkouts.Select(AssetCheckoutResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetCheckoutByIdAsync(
        string id,
        IAssetCheckoutRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset checkout id." });
        }

        var checkout = await repository.GetByIdAsync(id, cancellationToken);
        if (checkout is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetCheckout", checkout.CheckoutId, checkout.AssetName, EventAction.Viewed, cancellationToken);
        return Results.Ok(AssetCheckoutResponse.FromEntity(checkout));
    }

    private static async Task<IResult> GetAssetCheckoutsByAssetIdAsync(
        string assetId,
        IAssetCheckoutRepository repository,
        CancellationToken cancellationToken)
    {
        var checkouts = await repository.GetByAssetIdAsync(assetId, cancellationToken);
        return Results.Ok(checkouts.Select(AssetCheckoutResponse.FromEntity));
    }

    private static async Task<IResult> CreateAssetCheckoutAsync(
        CreateAssetCheckoutRequest request,
        IAssetCheckoutRepository repository,
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
        var checkoutId = $"{CheckoutIdPrefix}{nextSequence:D6}";

        var checkout = await repository.CreateAsync(request.ToEntity(checkoutId), cancellationToken);
        await eventLogger.LogAsync("AssetCheckout", checkout.CheckoutId, checkout.AssetName, EventAction.Created, cancellationToken);
        return Results.Created($"/api/asset-checkouts/{checkout.Id}", AssetCheckoutResponse.FromEntity(checkout));
    }

    private static async Task<IResult> UpdateAssetCheckoutAsync(
        string id,
        UpdateAssetCheckoutRequest request,
        IAssetCheckoutRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset checkout id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var checkout = await repository.GetByIdAsync(id, cancellationToken);
        if (checkout is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(checkout);

        var updated = await repository.UpdateAsync(id, checkout, cancellationToken);
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetCheckout", checkout.CheckoutId, checkout.AssetName, EventAction.Updated, cancellationToken);
        return Results.Ok(AssetCheckoutResponse.FromEntity(checkout));
    }

    private static async Task<IResult> DeleteAssetCheckoutAsync(
        string id,
        IAssetCheckoutRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset checkout id." });
        }

        var checkout = await repository.GetByIdAsync(id, cancellationToken);
        if (checkout is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetCheckout", checkout.CheckoutId, checkout.AssetName, EventAction.Deleted, cancellationToken);
        return Results.NoContent();
    }
}
