using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetLifecycleEntity = A360.Asset.Domain.Entities.AssetLifecycle;

namespace A360.Asset.Repository.Repositories;

public sealed class AssetLifecycleRepository : MongoRepository<AssetLifecycleEntity>, IAssetLifecycleRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "asset_lifecycles";

    public AssetLifecycleRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetLifecycleEntity>(CollectionName))
    {
    }

    public async Task<AssetLifecycleEntity?> GetByLifecycleIdAsync(string lifecycleId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(lifecycle => lifecycle.LifecycleId == lifecycleId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<AssetLifecycleEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(lifecycle => lifecycle.AssetId == assetId)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetLifecycleEntity>(
                Builders<AssetLifecycleEntity>.IndexKeys.Ascending(lifecycle => lifecycle.LifecycleId),
                new CreateIndexOptions { Name = "ix_asset_lifecycles_lifecycle_id", Unique = true }),
            new CreateIndexModel<AssetLifecycleEntity>(
                Builders<AssetLifecycleEntity>.IndexKeys.Ascending(lifecycle => lifecycle.AssetId),
                new CreateIndexOptions { Name = "ix_asset_lifecycles_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
