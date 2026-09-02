using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetTypeEntity = A360.MasterManagement.Domain.Entities.AssetType;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class AssetTypeRepository : MongoRepository<AssetTypeEntity>, IAssetTypeRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "asset_types";

    public AssetTypeRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetTypeEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetTypeEntity>(
                Builders<AssetTypeEntity>.IndexKeys.Ascending(assetType => assetType.AssetTypeId),
                new CreateIndexOptions { Name = "ix_asset_types_asset_type_id", Unique = true }),
            new CreateIndexModel<AssetTypeEntity>(
                Builders<AssetTypeEntity>.IndexKeys.Ascending(assetType => assetType.AssetId),
                new CreateIndexOptions { Name = "ix_asset_types_asset_id" }),
            new CreateIndexModel<AssetTypeEntity>(
                Builders<AssetTypeEntity>.IndexKeys.Ascending(assetType => assetType.AssetTypeCode),
                new CreateIndexOptions { Name = "ix_asset_types_asset_type_code" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
