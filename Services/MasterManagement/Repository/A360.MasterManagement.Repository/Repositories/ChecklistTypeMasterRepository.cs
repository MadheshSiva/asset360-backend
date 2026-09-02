using MongoDB.Driver;
using A360.Repository.Repositories;
using ChecklistTypeMasterEntity = A360.MasterManagement.Domain.Entities.ChecklistTypeMaster;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class ChecklistTypeMasterRepository : MongoRepository<ChecklistTypeMasterEntity>, IChecklistTypeMasterRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "checklist_type_masters";

    public ChecklistTypeMasterRepository(IMongoDatabase database)
        : base(database.GetCollection<ChecklistTypeMasterEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<ChecklistTypeMasterEntity>(
                Builders<ChecklistTypeMasterEntity>.IndexKeys.Ascending(checklistTypeMaster => checklistTypeMaster.TypeId),
                new CreateIndexOptions { Name = "ix_checklist_type_masters_type_id", Unique = true }),
            new CreateIndexModel<ChecklistTypeMasterEntity>(
                Builders<ChecklistTypeMasterEntity>.IndexKeys.Ascending(checklistTypeMaster => checklistTypeMaster.AssetId),
                new CreateIndexOptions { Name = "ix_checklist_type_masters_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
