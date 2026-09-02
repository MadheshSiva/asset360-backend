using MongoDB.Driver;
using A360.Repository.Repositories;
using PermitTypeMasterEntity = A360.MasterManagement.Domain.Entities.PermitTypeMaster;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class PermitTypeMasterRepository : MongoRepository<PermitTypeMasterEntity>, IPermitTypeMasterRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "permit_type_masters";

    public PermitTypeMasterRepository(IMongoDatabase database)
        : base(database.GetCollection<PermitTypeMasterEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<PermitTypeMasterEntity>(
                Builders<PermitTypeMasterEntity>.IndexKeys.Ascending(permitTypeMaster => permitTypeMaster.PermitTypeId),
                new CreateIndexOptions { Name = "ix_permit_type_masters_permit_type_id", Unique = true }),
            new CreateIndexModel<PermitTypeMasterEntity>(
                Builders<PermitTypeMasterEntity>.IndexKeys.Ascending(permitTypeMaster => permitTypeMaster.AssetId),
                new CreateIndexOptions { Name = "ix_permit_type_masters_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
