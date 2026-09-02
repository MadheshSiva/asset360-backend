using MongoDB.Driver;
using A360.Repository.Repositories;
using PriorityEntity = A360.MasterManagement.Domain.Entities.Priority;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class PriorityRepository : MongoRepository<PriorityEntity>, IPriorityRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "priorities";

    public PriorityRepository(IMongoDatabase database)
        : base(database.GetCollection<PriorityEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<PriorityEntity>(
                Builders<PriorityEntity>.IndexKeys.Ascending(priority => priority.PriorityId),
                new CreateIndexOptions { Name = "ix_priorities_priority_id", Unique = true }),
            new CreateIndexModel<PriorityEntity>(
                Builders<PriorityEntity>.IndexKeys.Ascending(priority => priority.AssetId),
                new CreateIndexOptions { Name = "ix_priorities_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
