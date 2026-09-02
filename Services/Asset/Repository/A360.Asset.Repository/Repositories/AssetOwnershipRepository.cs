using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetOwnershipEntity = A360.Asset.Domain.Entities.AssetOwnership;

namespace A360.Asset.Repository.Repositories;

public sealed class AssetOwnershipRepository : MongoRepository<AssetOwnershipEntity>, IAssetOwnershipRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "asset_ownerships";

    public AssetOwnershipRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetOwnershipEntity>(CollectionName))
    {
    }

    public async Task<AssetOwnershipEntity?> GetByOwnershipIdAsync(string ownershipId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(ownership => ownership.OwnershipId == ownershipId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<AssetOwnershipEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(ownership => ownership.AssetId == assetId)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetOwnershipEntity>(
                Builders<AssetOwnershipEntity>.IndexKeys.Ascending(ownership => ownership.OwnershipId),
                new CreateIndexOptions { Name = "ix_asset_ownerships_ownership_id", Unique = true }),
            new CreateIndexModel<AssetOwnershipEntity>(
                Builders<AssetOwnershipEntity>.IndexKeys.Ascending(ownership => ownership.AssetId),
                new CreateIndexOptions { Name = "ix_asset_ownerships_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
