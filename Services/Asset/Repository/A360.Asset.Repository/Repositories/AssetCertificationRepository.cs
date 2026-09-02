using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetCertificationEntity = A360.Asset.Domain.Entities.AssetCertification;

namespace A360.Asset.Repository.Repositories;

public sealed class AssetCertificationRepository : MongoRepository<AssetCertificationEntity>, IAssetCertificationRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "asset_certifications";

    public AssetCertificationRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetCertificationEntity>(CollectionName))
    {
    }

    public async Task<AssetCertificationEntity?> GetByCertificationIdAsync(string certificationId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(certification => certification.CertificationId == certificationId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<AssetCertificationEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(certification => certification.AssetId == assetId)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetCertificationEntity>(
                Builders<AssetCertificationEntity>.IndexKeys.Ascending(certification => certification.CertificationId),
                new CreateIndexOptions { Name = "ix_asset_certifications_certification_id", Unique = true }),
            new CreateIndexModel<AssetCertificationEntity>(
                Builders<AssetCertificationEntity>.IndexKeys.Ascending(certification => certification.AssetId),
                new CreateIndexOptions { Name = "ix_asset_certifications_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
