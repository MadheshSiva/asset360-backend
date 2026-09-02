using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetAuditAndVerificationEntity = A360.Asset.Domain.Entities.AssetAuditAndVerification;

namespace A360.Asset.Repository.Repositories;

public sealed class AssetAuditAndVerificationRepository : MongoRepository<AssetAuditAndVerificationEntity>, IAssetAuditAndVerificationRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "asset_audit_and_verifications";

    public AssetAuditAndVerificationRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetAuditAndVerificationEntity>(CollectionName))
    {
    }

    public async Task<AssetAuditAndVerificationEntity?> GetByAuditVerificationIdAsync(string auditVerificationId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(record => record.AuditVerificationId == auditVerificationId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<AssetAuditAndVerificationEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(record => record.AssetId == assetId)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetAuditAndVerificationEntity>(
                Builders<AssetAuditAndVerificationEntity>.IndexKeys.Ascending(record => record.AuditVerificationId),
                new CreateIndexOptions { Name = "ix_asset_audit_and_verifications_audit_verification_id", Unique = true }),
            new CreateIndexModel<AssetAuditAndVerificationEntity>(
                Builders<AssetAuditAndVerificationEntity>.IndexKeys.Ascending(record => record.AssetId),
                new CreateIndexOptions { Name = "ix_asset_audit_and_verifications_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
