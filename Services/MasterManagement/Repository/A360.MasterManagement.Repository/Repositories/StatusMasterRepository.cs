using MongoDB.Driver;
using A360.Repository.Repositories;
using StatusMasterEntity = A360.MasterManagement.Domain.Entities.StatusMaster;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class StatusMasterRepository : MongoRepository<StatusMasterEntity>, IStatusMasterRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "status_masters";

    public StatusMasterRepository(IMongoDatabase database)
        : base(database.GetCollection<StatusMasterEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<StatusMasterEntity>(
                Builders<StatusMasterEntity>.IndexKeys.Ascending(statusMaster => statusMaster.StatusId),
                new CreateIndexOptions { Name = "ix_status_masters_status_id", Unique = true }),
            new CreateIndexModel<StatusMasterEntity>(
                Builders<StatusMasterEntity>.IndexKeys.Ascending(statusMaster => statusMaster.AssetId),
                new CreateIndexOptions { Name = "ix_status_masters_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
