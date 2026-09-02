using MongoDB.Driver;
using A360.Repository.Repositories;
using PermissionMasterEntity = A360.MasterManagement.Domain.Entities.PermissionMaster;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class PermissionMasterRepository : MongoRepository<PermissionMasterEntity>, IPermissionMasterRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "permission_masters";

    public PermissionMasterRepository(IMongoDatabase database)
        : base(database.GetCollection<PermissionMasterEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<PermissionMasterEntity>(
                Builders<PermissionMasterEntity>.IndexKeys.Ascending(permissionMaster => permissionMaster.PermissionId),
                new CreateIndexOptions { Name = "ix_permission_masters_permission_id", Unique = true }),
            new CreateIndexModel<PermissionMasterEntity>(
                Builders<PermissionMasterEntity>.IndexKeys.Ascending(permissionMaster => permissionMaster.AssetId),
                new CreateIndexOptions { Name = "ix_permission_masters_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
