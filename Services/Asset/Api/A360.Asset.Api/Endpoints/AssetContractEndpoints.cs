using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.Asset.Api.Endpoints;

public static class AssetContractEndpoints
{
    private const string SequenceName = "asset-contract";
    private const string ContractIdPrefix = "CON";

    public static RouteGroupBuilder MapAssetContractEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/asset-contracts").WithTags("AssetContracts");

        group.MapGet("", GetAssetContractsAsync).WithName("GetAssetContracts");
        group.MapGet("/{id}", GetAssetContractByIdAsync).WithName("GetAssetContractById");
        group.MapGet("/by-asset/{assetId}", GetAssetContractsByAssetIdAsync).WithName("GetAssetContractsByAssetId");
        group.MapPost("", CreateAssetContractAsync).WithName("CreateAssetContract");
        group.MapPut("/{id}", UpdateAssetContractAsync).WithName("UpdateAssetContract");
        group.MapDelete("/{id}", DeleteAssetContractAsync).WithName("DeleteAssetContract");

        return group;
    }

    private static async Task<IResult> GetAssetContractsAsync(
        IAssetContractRepository repository,
        CancellationToken cancellationToken)
    {
        var contracts = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(contracts.Select(AssetContractResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetContractByIdAsync(
        string id,
        IAssetContractRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset contract id." });
        }

        var contract = await repository.GetByIdAsync(id, cancellationToken);
        return contract is null ? Results.NotFound() : Results.Ok(AssetContractResponse.FromEntity(contract));
    }

    private static async Task<IResult> GetAssetContractsByAssetIdAsync(
        string assetId,
        IAssetContractRepository repository,
        CancellationToken cancellationToken)
    {
        var contracts = await repository.GetByAssetIdAsync(assetId, cancellationToken);
        return Results.Ok(contracts.Select(AssetContractResponse.FromEntity));
    }

    private static async Task<IResult> CreateAssetContractAsync(
        CreateAssetContractRequest request,
        IAssetContractRepository repository,
        ISequenceGenerator sequenceGenerator,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var contractId = $"{ContractIdPrefix}{nextSequence:D6}";

        var contract = await repository.CreateAsync(request.ToEntity(contractId), cancellationToken);
        return Results.Created($"/api/asset-contracts/{contract.Id}", AssetContractResponse.FromEntity(contract));
    }

    private static async Task<IResult> UpdateAssetContractAsync(
        string id,
        UpdateAssetContractRequest request,
        IAssetContractRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset contract id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var contract = await repository.GetByIdAsync(id, cancellationToken);
        if (contract is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(contract);

        var updated = await repository.UpdateAsync(id, contract, cancellationToken);
        return updated ? Results.Ok(AssetContractResponse.FromEntity(contract)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteAssetContractAsync(
        string id,
        IAssetContractRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset contract id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
