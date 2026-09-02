using MongoDB.Driver;
using A360.Repository.Repositories;
using ModuleAccessMasterEntity = A360.MasterManagement.Domain.Entities.ModuleAccessMaster;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class ModuleAccessMasterRepository : MongoRepository<ModuleAccessMasterEntity>, IModuleAccessMasterRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "module_access_masters";

    public ModuleAccessMasterRepository(IMongoDatabase database)
        : base(database.GetCollection<ModuleAccessMasterEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<ModuleAccessMasterEntity>(
                Builders<ModuleAccessMasterEntity>.IndexKeys.Ascending(moduleAccessMaster => moduleAccessMaster.ModuleId),
                new CreateIndexOptions { Name = "ix_module_access_masters_module_id", Unique = true }),
            new CreateIndexModel<ModuleAccessMasterEntity>(
                Builders<ModuleAccessMasterEntity>.IndexKeys.Ascending(moduleAccessMaster => moduleAccessMaster.AssetId),
                new CreateIndexOptions { Name = "ix_module_access_masters_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
