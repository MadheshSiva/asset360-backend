using MongoDB.Driver;
using A360.Repository.Repositories;
using UpdateSourceMasterEntity = A360.MasterManagement.Domain.Entities.UpdateSourceMaster;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class UpdateSourceMasterRepository : MongoRepository<UpdateSourceMasterEntity>, IUpdateSourceMasterRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "update_source_masters";

    public UpdateSourceMasterRepository(IMongoDatabase database)
        : base(database.GetCollection<UpdateSourceMasterEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<UpdateSourceMasterEntity>(
                Builders<UpdateSourceMasterEntity>.IndexKeys.Ascending(updateSourceMaster => updateSourceMaster.SourceId),
                new CreateIndexOptions { Name = "ix_update_source_masters_source_id", Unique = true }),
            new CreateIndexModel<UpdateSourceMasterEntity>(
                Builders<UpdateSourceMasterEntity>.IndexKeys.Ascending(updateSourceMaster => updateSourceMaster.AssetId),
                new CreateIndexOptions { Name = "ix_update_source_masters_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
