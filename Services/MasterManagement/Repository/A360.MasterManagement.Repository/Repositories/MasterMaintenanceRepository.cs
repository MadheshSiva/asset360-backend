using MongoDB.Driver;
using A360.Repository.Repositories;
using MasterMaintenanceEntity = A360.MasterManagement.Domain.Entities.MasterMaintenance;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class MasterMaintenanceRepository : MongoRepository<MasterMaintenanceEntity>, IMasterMaintenanceRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "master_maintenances";

    public MasterMaintenanceRepository(IMongoDatabase database)
        : base(database.GetCollection<MasterMaintenanceEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<MasterMaintenanceEntity>(
                Builders<MasterMaintenanceEntity>.IndexKeys.Ascending(masterMaintenance => masterMaintenance.MasterMaintenanceId),
                new CreateIndexOptions { Name = "ix_master_maintenances_master_maintenance_id", Unique = true }),
            new CreateIndexModel<MasterMaintenanceEntity>(
                Builders<MasterMaintenanceEntity>.IndexKeys.Ascending(masterMaintenance => masterMaintenance.AssetId),
                new CreateIndexOptions { Name = "ix_master_maintenances_asset_id" }),
            new CreateIndexModel<MasterMaintenanceEntity>(
                Builders<MasterMaintenanceEntity>.IndexKeys.Ascending(masterMaintenance => masterMaintenance.MasterMaintenanceCode),
                new CreateIndexOptions { Name = "ix_master_maintenances_master_maintenance_code" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
