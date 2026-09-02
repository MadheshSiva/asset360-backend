using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetCheckoutEntity = A360.Asset.Domain.Entities.AssetCheckout;

namespace A360.Asset.Repository.Repositories;

public sealed class AssetCheckoutRepository : MongoRepository<AssetCheckoutEntity>, IAssetCheckoutRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "asset_checkouts";

    public AssetCheckoutRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetCheckoutEntity>(CollectionName))
    {
    }

    public async Task<AssetCheckoutEntity?> GetByCheckoutIdAsync(string checkoutId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(checkout => checkout.CheckoutId == checkoutId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<AssetCheckoutEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(checkout => checkout.AssetId == assetId)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetCheckoutEntity>(
                Builders<AssetCheckoutEntity>.IndexKeys.Ascending(checkout => checkout.CheckoutId),
                new CreateIndexOptions { Name = "ix_asset_checkouts_checkout_id", Unique = true }),
            new CreateIndexModel<AssetCheckoutEntity>(
                Builders<AssetCheckoutEntity>.IndexKeys.Ascending(checkout => checkout.AssetId),
                new CreateIndexOptions { Name = "ix_asset_checkouts_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
