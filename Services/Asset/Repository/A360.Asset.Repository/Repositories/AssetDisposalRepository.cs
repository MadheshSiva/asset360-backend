using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetDisposalEntity = A360.Asset.Domain.Entities.AssetDisposal;

namespace A360.Asset.Repository.Repositories;

public sealed class AssetDisposalRepository : MongoRepository<AssetDisposalEntity>, IAssetDisposalRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "asset_disposals";

    public AssetDisposalRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetDisposalEntity>(CollectionName))
    {
    }

    public async Task<AssetDisposalEntity?> GetByDisposalIdAsync(string disposalId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(disposal => disposal.DisposalId == disposalId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<AssetDisposalEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(disposal => disposal.AssetId == assetId)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetDisposalEntity>(
                Builders<AssetDisposalEntity>.IndexKeys.Ascending(disposal => disposal.DisposalId),
                new CreateIndexOptions { Name = "ix_asset_disposals_disposal_id", Unique = true }),
            new CreateIndexModel<AssetDisposalEntity>(
                Builders<AssetDisposalEntity>.IndexKeys.Ascending(disposal => disposal.AssetId),
                new CreateIndexOptions { Name = "ix_asset_disposals_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
