using MongoDB.Driver;
using A360.Repository.Repositories;
using ResponseTypeMasterEntity = A360.MasterManagement.Domain.Entities.ResponseTypeMaster;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class ResponseTypeMasterRepository : MongoRepository<ResponseTypeMasterEntity>, IResponseTypeMasterRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "response_type_masters";

    public ResponseTypeMasterRepository(IMongoDatabase database)
        : base(database.GetCollection<ResponseTypeMasterEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<ResponseTypeMasterEntity>(
                Builders<ResponseTypeMasterEntity>.IndexKeys.Ascending(responseTypeMaster => responseTypeMaster.TypeId),
                new CreateIndexOptions { Name = "ix_response_type_masters_type_id", Unique = true }),
            new CreateIndexModel<ResponseTypeMasterEntity>(
                Builders<ResponseTypeMasterEntity>.IndexKeys.Ascending(responseTypeMaster => responseTypeMaster.AssetId),
                new CreateIndexOptions { Name = "ix_response_type_masters_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
