using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;
using A360.Domain.Entities;
using A360.Repository.Activity;

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
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset contract id." });
        }

        var contract = await repository.GetByIdAsync(id, cancellationToken);
        if (contract is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetContract", contract.ContractId, contract.AssetName, EventAction.Viewed, cancellationToken);
        return Results.Ok(AssetContractResponse.FromEntity(contract));
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
        IEventLogger eventLogger,
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
        await eventLogger.LogAsync("AssetContract", contract.ContractId, contract.AssetName, EventAction.Created, cancellationToken);
        return Results.Created($"/api/asset-contracts/{contract.Id}", AssetContractResponse.FromEntity(contract));
    }

    private static async Task<IResult> UpdateAssetContractAsync(
        string id,
        UpdateAssetContractRequest request,
        IAssetContractRepository repository,
        IEventLogger eventLogger,
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
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetContract", contract.ContractId, contract.AssetName, EventAction.Updated, cancellationToken);
        return Results.Ok(AssetContractResponse.FromEntity(contract));
    }

    private static async Task<IResult> DeleteAssetContractAsync(
        string id,
        IAssetContractRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset contract id." });
        }

        var contract = await repository.GetByIdAsync(id, cancellationToken);
        if (contract is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("AssetContract", contract.ContractId, contract.AssetName, EventAction.Deleted, cancellationToken);
        return Results.NoContent();
    }
}
