using MongoDB.Driver;
using A360.Repository.Repositories;
using ConditionMasterEntity = A360.MasterManagement.Domain.Entities.ConditionMaster;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class ConditionMasterRepository : MongoRepository<ConditionMasterEntity>, IConditionMasterRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "condition_masters";

    public ConditionMasterRepository(IMongoDatabase database)
        : base(database.GetCollection<ConditionMasterEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<ConditionMasterEntity>(
                Builders<ConditionMasterEntity>.IndexKeys.Ascending(conditionMaster => conditionMaster.ConditionId),
                new CreateIndexOptions { Name = "ix_condition_masters_condition_id", Unique = true }),
            new CreateIndexModel<ConditionMasterEntity>(
                Builders<ConditionMasterEntity>.IndexKeys.Ascending(conditionMaster => conditionMaster.AssetId),
                new CreateIndexOptions { Name = "ix_condition_masters_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
