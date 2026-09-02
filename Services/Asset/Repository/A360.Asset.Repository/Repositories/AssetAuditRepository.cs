using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetAuditEntity = A360.Asset.Domain.Entities.AssetAudit;

namespace A360.Asset.Repository.Repositories;

public sealed class AssetAuditRepository : MongoRepository<AssetAuditEntity>, IAssetAuditRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "asset_audits";

    public AssetAuditRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetAuditEntity>(CollectionName))
    {
    }

    public async Task<AssetAuditEntity?> GetByAuditIdAsync(string auditId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(audit => audit.AuditId == auditId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<AssetAuditEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(audit => audit.AssetId == assetId)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetAuditEntity>(
                Builders<AssetAuditEntity>.IndexKeys.Ascending(audit => audit.AuditId),
                new CreateIndexOptions { Name = "ix_asset_audits_audit_id", Unique = true }),
            new CreateIndexModel<AssetAuditEntity>(
                Builders<AssetAuditEntity>.IndexKeys.Ascending(audit => audit.AssetId),
                new CreateIndexOptions { Name = "ix_asset_audits_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
