using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetActivityEntity = A360.Asset.Domain.Entities.AssetActivity;

namespace A360.Asset.Repository.Repositories;

public sealed class AssetActivityRepository : MongoRepository<AssetActivityEntity>, IAssetActivityRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "asset_activities";

    public AssetActivityRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetActivityEntity>(CollectionName))
    {
    }

    public async Task<AssetActivityEntity?> GetByActivityIdAsync(string activityId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(activity => activity.ActivityId == activityId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<AssetActivityEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(activity => activity.AssetId == assetId)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetActivityEntity>(
                Builders<AssetActivityEntity>.IndexKeys.Ascending(activity => activity.ActivityId),
                new CreateIndexOptions { Name = "ix_asset_activities_activity_id", Unique = true }),
            new CreateIndexModel<AssetActivityEntity>(
                Builders<AssetActivityEntity>.IndexKeys.Ascending(activity => activity.AssetId),
                new CreateIndexOptions { Name = "ix_asset_activities_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
