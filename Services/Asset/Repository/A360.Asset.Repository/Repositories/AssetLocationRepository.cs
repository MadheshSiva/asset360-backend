using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetLocationEntity = A360.Asset.Domain.Entities.AssetLocation;

namespace A360.Asset.Repository.Repositories;

public sealed class AssetLocationRepository : MongoRepository<AssetLocationEntity>, IAssetLocationRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "asset_locations";

    public AssetLocationRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetLocationEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetLocationEntity>(
                Builders<AssetLocationEntity>.IndexKeys.Ascending(location => location.AssetId),
                new CreateIndexOptions { Name = "ix_asset_locations_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
