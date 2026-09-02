using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetUtilizationAndPerformanceEntity = A360.Asset.Domain.Entities.AssetUtilizationAndPerformance;

namespace A360.Asset.Repository.Repositories;

public sealed class AssetUtilizationAndPerformanceRepository : MongoRepository<AssetUtilizationAndPerformanceEntity>, IAssetUtilizationAndPerformanceRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "asset_utilization_and_performances";

    public AssetUtilizationAndPerformanceRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetUtilizationAndPerformanceEntity>(CollectionName))
    {
    }

    public async Task<AssetUtilizationAndPerformanceEntity?> GetByUtilizationPerformanceIdAsync(string utilizationPerformanceId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(record => record.UtilizationPerformanceId == utilizationPerformanceId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<AssetUtilizationAndPerformanceEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(record => record.AssetId == assetId)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetUtilizationAndPerformanceEntity>(
                Builders<AssetUtilizationAndPerformanceEntity>.IndexKeys.Ascending(record => record.UtilizationPerformanceId),
                new CreateIndexOptions { Name = "ix_asset_utilization_and_performances_utilization_performance_id", Unique = true }),
            new CreateIndexModel<AssetUtilizationAndPerformanceEntity>(
                Builders<AssetUtilizationAndPerformanceEntity>.IndexKeys.Ascending(record => record.AssetId),
                new CreateIndexOptions { Name = "ix_asset_utilization_and_performances_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
