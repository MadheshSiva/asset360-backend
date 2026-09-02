using MongoDB.Driver;
using A360.Repository.Repositories;
using TaggedAssetsEntity = A360.Asset.Domain.Entities.TaggedAssets;

namespace A360.Asset.Repository.Repositories;

public sealed class TaggedAssetsRepository : MongoRepository<TaggedAssetsEntity>, ITaggedAssetsRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "tagged_assets";

    public TaggedAssetsRepository(IMongoDatabase database)
        : base(database.GetCollection<TaggedAssetsEntity>(CollectionName))
    {
    }

    public async Task<TaggedAssetsEntity?> GetByTaggedAssetIdAsync(string taggedAssetId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(taggedAsset => taggedAsset.TaggedAssetId == taggedAssetId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<TaggedAssetsEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(taggedAsset => taggedAsset.AssetId == assetId)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<TaggedAssetsEntity>(
                Builders<TaggedAssetsEntity>.IndexKeys.Ascending(taggedAsset => taggedAsset.TaggedAssetId),
                new CreateIndexOptions { Name = "ix_tagged_assets_tagged_asset_id", Unique = true }),
            new CreateIndexModel<TaggedAssetsEntity>(
                Builders<TaggedAssetsEntity>.IndexKeys.Ascending(taggedAsset => taggedAsset.AssetId),
                new CreateIndexOptions { Name = "ix_tagged_assets_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
