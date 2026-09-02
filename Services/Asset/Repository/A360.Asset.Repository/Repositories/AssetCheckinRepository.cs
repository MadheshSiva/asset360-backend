using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetCheckinEntity = A360.Asset.Domain.Entities.AssetCheckin;

namespace A360.Asset.Repository.Repositories;

public sealed class AssetCheckinRepository : MongoRepository<AssetCheckinEntity>, IAssetCheckinRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "asset_checkins";

    public AssetCheckinRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetCheckinEntity>(CollectionName))
    {
    }

    public async Task<AssetCheckinEntity?> GetByCheckinIdAsync(string checkinId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(checkin => checkin.CheckinId == checkinId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<AssetCheckinEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(checkin => checkin.AssetId == assetId)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetCheckinEntity>(
                Builders<AssetCheckinEntity>.IndexKeys.Ascending(checkin => checkin.CheckinId),
                new CreateIndexOptions { Name = "ix_asset_checkins_checkin_id", Unique = true }),
            new CreateIndexModel<AssetCheckinEntity>(
                Builders<AssetCheckinEntity>.IndexKeys.Ascending(checkin => checkin.AssetId),
                new CreateIndexOptions { Name = "ix_asset_checkins_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
