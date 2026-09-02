using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetDomainEntity = A360.Asset.Domain.Entities.AssetDomain;

namespace A360.Asset.Repository.Repositories;

public sealed class AssetDomainRepository : MongoRepository<AssetDomainEntity>, IAssetDomainRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "asset_domains";

    public AssetDomainRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetDomainEntity>(CollectionName))
    {
    }

    public async Task<AssetDomainEntity?> GetByAssetDomainIdAsync(string assetDomainId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(record => record.AssetDomainId == assetDomainId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<AssetDomainEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(record => record.AssetId == assetId)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetDomainEntity>(
                Builders<AssetDomainEntity>.IndexKeys.Ascending(record => record.AssetDomainId),
                new CreateIndexOptions { Name = "ix_asset_domains_asset_domain_id", Unique = true }),
            new CreateIndexModel<AssetDomainEntity>(
                Builders<AssetDomainEntity>.IndexKeys.Ascending(record => record.AssetId),
                new CreateIndexOptions { Name = "ix_asset_domains_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
