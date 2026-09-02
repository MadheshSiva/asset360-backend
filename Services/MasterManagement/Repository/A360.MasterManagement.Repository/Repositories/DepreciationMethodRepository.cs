using MongoDB.Driver;
using A360.Repository.Repositories;
using DepreciationMethodEntity = A360.MasterManagement.Domain.Entities.DepreciationMethod;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class DepreciationMethodRepository : MongoRepository<DepreciationMethodEntity>, IDepreciationMethodRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "depreciation_methods";

    public DepreciationMethodRepository(IMongoDatabase database)
        : base(database.GetCollection<DepreciationMethodEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<DepreciationMethodEntity>(
                Builders<DepreciationMethodEntity>.IndexKeys.Ascending(depreciationMethod => depreciationMethod.MethodId),
                new CreateIndexOptions { Name = "ix_depreciation_methods_method_id", Unique = true }),
            new CreateIndexModel<DepreciationMethodEntity>(
                Builders<DepreciationMethodEntity>.IndexKeys.Ascending(depreciationMethod => depreciationMethod.MethodCode),
                new CreateIndexOptions { Name = "ix_depreciation_methods_method_code" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
