using MongoDB.Driver;
using A360.Repository.Repositories;
using ResourceTypeEntity = A360.MasterManagement.Domain.Entities.ResourceType;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class ResourceTypeRepository : MongoRepository<ResourceTypeEntity>, IResourceTypeRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "resource_types";

    public ResourceTypeRepository(IMongoDatabase database)
        : base(database.GetCollection<ResourceTypeEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<ResourceTypeEntity>(
                Builders<ResourceTypeEntity>.IndexKeys.Ascending(resourceType => resourceType.TypeId),
                new CreateIndexOptions { Name = "ix_resource_types_type_id", Unique = true }),
            new CreateIndexModel<ResourceTypeEntity>(
                Builders<ResourceTypeEntity>.IndexKeys.Ascending(resourceType => resourceType.AssetId),
                new CreateIndexOptions { Name = "ix_resource_types_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
