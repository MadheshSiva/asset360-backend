using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetEntity = A360.Asset.Domain.Entities.Asset;

namespace A360.Asset.Repository.Repositories;

public sealed class AssetRepository : MongoRepository<AssetEntity>, IAssetRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "assets";

    public AssetRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetEntity>(CollectionName))
    {
    }

    public async Task<AssetEntity?> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(asset => asset.AssetId == assetId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetEntity>(
                Builders<AssetEntity>.IndexKeys.Ascending(asset => asset.AssetId),
                new CreateIndexOptions { Name = "ix_assets_asset_id", Unique = true }),
            new CreateIndexModel<AssetEntity>(
                Builders<AssetEntity>.IndexKeys.Ascending(asset => asset.SerialNumber),
                new CreateIndexOptions { Name = "ix_assets_serial_number" }),
            new CreateIndexModel<AssetEntity>(
                Builders<AssetEntity>.IndexKeys.Ascending(asset => asset.ParentAsset),
                new CreateIndexOptions { Name = "ix_assets_parent_asset" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
