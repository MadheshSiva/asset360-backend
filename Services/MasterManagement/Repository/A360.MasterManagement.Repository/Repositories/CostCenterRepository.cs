using MongoDB.Driver;
using A360.Repository.Repositories;
using CostCenterEntity = A360.MasterManagement.Domain.Entities.CostCenter;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class CostCenterRepository : MongoRepository<CostCenterEntity>, ICostCenterRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "cost_centers";

    public CostCenterRepository(IMongoDatabase database)
        : base(database.GetCollection<CostCenterEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<CostCenterEntity>(
                Builders<CostCenterEntity>.IndexKeys.Ascending(costCenter => costCenter.CostCenterId),
                new CreateIndexOptions { Name = "ix_cost_centers_cost_center_id", Unique = true }),
            new CreateIndexModel<CostCenterEntity>(
                Builders<CostCenterEntity>.IndexKeys.Ascending(costCenter => costCenter.CostCenterCode),
                new CreateIndexOptions { Name = "ix_cost_centers_cost_center_code" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
