using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetMaintenanceAndServiceEntity = A360.Asset.Domain.Entities.AssetMaintenanceAndService;

namespace A360.Asset.Repository.Repositories;

public sealed class AssetMaintenanceAndServiceRepository : MongoRepository<AssetMaintenanceAndServiceEntity>, IAssetMaintenanceAndServiceRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "asset_maintenance_and_services";

    public AssetMaintenanceAndServiceRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetMaintenanceAndServiceEntity>(CollectionName))
    {
    }

    public async Task<AssetMaintenanceAndServiceEntity?> GetByMaintenanceServiceIdAsync(string maintenanceServiceId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(service => service.MaintenanceServiceId == maintenanceServiceId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<AssetMaintenanceAndServiceEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(service => service.AssetId == assetId)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetMaintenanceAndServiceEntity>(
                Builders<AssetMaintenanceAndServiceEntity>.IndexKeys.Ascending(service => service.MaintenanceServiceId),
                new CreateIndexOptions { Name = "ix_asset_maintenance_and_services_maintenance_service_id", Unique = true }),
            new CreateIndexModel<AssetMaintenanceAndServiceEntity>(
                Builders<AssetMaintenanceAndServiceEntity>.IndexKeys.Ascending(service => service.AssetId),
                new CreateIndexOptions { Name = "ix_asset_maintenance_and_services_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
