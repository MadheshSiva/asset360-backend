using MongoDB.Driver;
using A360.Repository.Repositories;
using StatusChangeEntity = A360.MasterManagement.Domain.Entities.StatusChange;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class StatusChangeRepository : MongoRepository<StatusChangeEntity>, IStatusChangeRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "status_changes";

    public StatusChangeRepository(IMongoDatabase database)
        : base(database.GetCollection<StatusChangeEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<StatusChangeEntity>(
                Builders<StatusChangeEntity>.IndexKeys.Ascending(statusChange => statusChange.StatusChangeId),
                new CreateIndexOptions { Name = "ix_status_changes_status_change_id", Unique = true }),
            new CreateIndexModel<StatusChangeEntity>(
                Builders<StatusChangeEntity>.IndexKeys.Ascending(statusChange => statusChange.AssetId),
                new CreateIndexOptions { Name = "ix_status_changes_asset_id" }),
            new CreateIndexModel<StatusChangeEntity>(
                Builders<StatusChangeEntity>.IndexKeys.Ascending(statusChange => statusChange.StatusCode),
                new CreateIndexOptions { Name = "ix_status_changes_status_code" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
