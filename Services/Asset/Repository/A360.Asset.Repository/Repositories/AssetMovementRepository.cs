using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetMovementEntity = A360.Asset.Domain.Entities.AssetMovement;

namespace A360.Asset.Repository.Repositories;

public sealed class AssetMovementRepository : MongoRepository<AssetMovementEntity>, IAssetMovementRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "asset_movements";

    public AssetMovementRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetMovementEntity>(CollectionName))
    {
    }

    public async Task<AssetMovementEntity?> GetByMovementIdAsync(string movementId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(movement => movement.MovementId == movementId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<AssetMovementEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(movement => movement.AssetId == assetId)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetMovementEntity>(
                Builders<AssetMovementEntity>.IndexKeys.Ascending(movement => movement.MovementId),
                new CreateIndexOptions { Name = "ix_asset_movements_movement_id", Unique = true }),
            new CreateIndexModel<AssetMovementEntity>(
                Builders<AssetMovementEntity>.IndexKeys.Ascending(movement => movement.AssetId),
                new CreateIndexOptions { Name = "ix_asset_movements_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
