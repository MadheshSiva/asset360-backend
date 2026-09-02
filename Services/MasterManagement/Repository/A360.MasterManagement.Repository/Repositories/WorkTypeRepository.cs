using MongoDB.Driver;
using A360.Repository.Repositories;
using WorkTypeEntity = A360.MasterManagement.Domain.Entities.WorkType;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class WorkTypeRepository : MongoRepository<WorkTypeEntity>, IWorkTypeRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "work_types";

    public WorkTypeRepository(IMongoDatabase database)
        : base(database.GetCollection<WorkTypeEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<WorkTypeEntity>(
                Builders<WorkTypeEntity>.IndexKeys.Ascending(workType => workType.WorkTypeId),
                new CreateIndexOptions { Name = "ix_work_types_work_type_id", Unique = true }),
            new CreateIndexModel<WorkTypeEntity>(
                Builders<WorkTypeEntity>.IndexKeys.Ascending(workType => workType.AssetId),
                new CreateIndexOptions { Name = "ix_work_types_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
