using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetTypeFieldEntity = A360.MasterManagement.Domain.Entities.AssetTypeField;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class AssetTypeFieldRepository : MongoRepository<AssetTypeFieldEntity>, IAssetTypeFieldRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "asset_type_fields";

    public AssetTypeFieldRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetTypeFieldEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetTypeFieldEntity>(
                Builders<AssetTypeFieldEntity>.IndexKeys.Ascending(assetTypeField => assetTypeField.FieldId),
                new CreateIndexOptions { Name = "ix_asset_type_fields_field_id", Unique = true }),
            new CreateIndexModel<AssetTypeFieldEntity>(
                Builders<AssetTypeFieldEntity>.IndexKeys.Ascending(assetTypeField => assetTypeField.AssetId),
                new CreateIndexOptions { Name = "ix_asset_type_fields_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
