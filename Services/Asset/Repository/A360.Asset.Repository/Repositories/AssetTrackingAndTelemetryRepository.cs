using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetTrackingAndTelemetryEntity = A360.Asset.Domain.Entities.AssetTrackingAndTelemetry;

namespace A360.Asset.Repository.Repositories;

public sealed class AssetTrackingAndTelemetryRepository : MongoRepository<AssetTrackingAndTelemetryEntity>, IAssetTrackingAndTelemetryRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "asset_tracking_and_telemetries";

    public AssetTrackingAndTelemetryRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetTrackingAndTelemetryEntity>(CollectionName))
    {
    }

    public async Task<AssetTrackingAndTelemetryEntity?> GetByTrackingIdAsync(string trackingId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(telemetry => telemetry.TrackingId == trackingId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<AssetTrackingAndTelemetryEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(telemetry => telemetry.AssetId == assetId)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetTrackingAndTelemetryEntity>(
                Builders<AssetTrackingAndTelemetryEntity>.IndexKeys.Ascending(telemetry => telemetry.TrackingId),
                new CreateIndexOptions { Name = "ix_asset_tracking_and_telemetries_tracking_id", Unique = true }),
            new CreateIndexModel<AssetTrackingAndTelemetryEntity>(
                Builders<AssetTrackingAndTelemetryEntity>.IndexKeys.Ascending(telemetry => telemetry.AssetId),
                new CreateIndexOptions { Name = "ix_asset_tracking_and_telemetries_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
