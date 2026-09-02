using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetContractEntity = A360.Asset.Domain.Entities.AssetContract;

namespace A360.Asset.Repository.Repositories;

public sealed class AssetContractRepository : MongoRepository<AssetContractEntity>, IAssetContractRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "asset_contracts";

    public AssetContractRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetContractEntity>(CollectionName))
    {
    }

    public async Task<AssetContractEntity?> GetByContractIdAsync(string contractId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(contract => contract.ContractId == contractId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<AssetContractEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(contract => contract.AssetId == assetId)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetContractEntity>(
                Builders<AssetContractEntity>.IndexKeys.Ascending(contract => contract.ContractId),
                new CreateIndexOptions { Name = "ix_asset_contracts_contract_id", Unique = true }),
            new CreateIndexModel<AssetContractEntity>(
                Builders<AssetContractEntity>.IndexKeys.Ascending(contract => contract.AssetId),
                new CreateIndexOptions { Name = "ix_asset_contracts_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
