using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.Asset.Api.Endpoints;

public static class AssetDomainEndpoints
{
    private const string SequenceName = "asset-domain";
    private const string AssetDomainIdPrefix = "ADM";

    public static RouteGroupBuilder MapAssetDomainEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/asset-domains").WithTags("AssetDomains");

        group.MapGet("", GetAssetDomainsAsync).WithName("GetAssetDomains");
        group.MapGet("/{id}", GetAssetDomainByIdAsync).WithName("GetAssetDomainById");
        group.MapGet("/by-asset/{assetId}", GetAssetDomainsByAssetIdAsync).WithName("GetAssetDomainsByAssetId");
        group.MapPost("", CreateAssetDomainAsync).WithName("CreateAssetDomain");
        group.MapPut("/{id}", UpdateAssetDomainAsync).WithName("UpdateAssetDomain");
        group.MapDelete("/{id}", DeleteAssetDomainAsync).WithName("DeleteAssetDomain");

        return group;
    }

    private static async Task<IResult> GetAssetDomainsAsync(
        IAssetDomainRepository repository,
        CancellationToken cancellationToken)
    {
        var records = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(records.Select(AssetDomainResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetDomainByIdAsync(
        string id,
        IAssetDomainRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset domain id." });
        }

        var record = await repository.GetByIdAsync(id, cancellationToken);
        return record is null ? Results.NotFound() : Results.Ok(AssetDomainResponse.FromEntity(record));
    }

    private static async Task<IResult> GetAssetDomainsByAssetIdAsync(
        string assetId,
        IAssetDomainRepository repository,
        CancellationToken cancellationToken)
    {
        var records = await repository.GetByAssetIdAsync(assetId, cancellationToken);
        return Results.Ok(records.Select(AssetDomainResponse.FromEntity));
    }

    private static async Task<IResult> CreateAssetDomainAsync(
        CreateAssetDomainRequest request,
        IAssetDomainRepository repository,
        ISequenceGenerator sequenceGenerator,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var assetDomainId = $"{AssetDomainIdPrefix}{nextSequence:D6}";

        var record = await repository.CreateAsync(request.ToEntity(assetDomainId), cancellationToken);
        return Results.Created($"/api/asset-domains/{record.Id}", AssetDomainResponse.FromEntity(record));
    }

    private static async Task<IResult> UpdateAssetDomainAsync(
        string id,
        UpdateAssetDomainRequest request,
        IAssetDomainRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset domain id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var record = await repository.GetByIdAsync(id, cancellationToken);
        if (record is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(record);

        var updated = await repository.UpdateAsync(id, record, cancellationToken);
        return updated ? Results.Ok(AssetDomainResponse.FromEntity(record)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteAssetDomainAsync(
        string id,
        IAssetDomainRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset domain id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
