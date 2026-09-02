using MongoDB.Driver;
using A360.Repository.Repositories;
using UnitMasterEntity = A360.MasterManagement.Domain.Entities.UnitMaster;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class UnitMasterRepository : MongoRepository<UnitMasterEntity>, IUnitMasterRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "unit_masters";

    public UnitMasterRepository(IMongoDatabase database)
        : base(database.GetCollection<UnitMasterEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<UnitMasterEntity>(
                Builders<UnitMasterEntity>.IndexKeys.Ascending(unitMaster => unitMaster.UnitId),
                new CreateIndexOptions { Name = "ix_unit_masters_unit_id", Unique = true }),
            new CreateIndexModel<UnitMasterEntity>(
                Builders<UnitMasterEntity>.IndexKeys.Ascending(unitMaster => unitMaster.AssetId),
                new CreateIndexOptions { Name = "ix_unit_masters_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
