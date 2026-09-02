using MongoDB.Driver;
using A360.Repository.Repositories;
using ApiSyncStatusMasterEntity = A360.MasterManagement.Domain.Entities.ApiSyncStatusMaster;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class ApiSyncStatusMasterRepository : MongoRepository<ApiSyncStatusMasterEntity>, IApiSyncStatusMasterRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "api_sync_status_masters";

    public ApiSyncStatusMasterRepository(IMongoDatabase database)
        : base(database.GetCollection<ApiSyncStatusMasterEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<ApiSyncStatusMasterEntity>(
                Builders<ApiSyncStatusMasterEntity>.IndexKeys.Ascending(apiSyncStatusMaster => apiSyncStatusMaster.StatusId),
                new CreateIndexOptions { Name = "ix_api_sync_status_masters_status_id", Unique = true }),
            new CreateIndexModel<ApiSyncStatusMasterEntity>(
                Builders<ApiSyncStatusMasterEntity>.IndexKeys.Ascending(apiSyncStatusMaster => apiSyncStatusMaster.AssetId),
                new CreateIndexOptions { Name = "ix_api_sync_status_masters_asset_id" }),
            new CreateIndexModel<ApiSyncStatusMasterEntity>(
                Builders<ApiSyncStatusMasterEntity>.IndexKeys.Ascending(apiSyncStatusMaster => apiSyncStatusMaster.StatusCode),
                new CreateIndexOptions { Name = "ix_api_sync_status_masters_status_code" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
