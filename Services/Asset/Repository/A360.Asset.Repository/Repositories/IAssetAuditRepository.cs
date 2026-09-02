using A360.Repository.Repositories;
using AssetAuditEntity = A360.Asset.Domain.Entities.AssetAudit;

namespace A360.Asset.Repository.Repositories;

public interface IAssetAuditRepository : IMongoRepository<AssetAuditEntity>
{
    Task<AssetAuditEntity?> GetByAuditIdAsync(string auditId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<AssetAuditEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
}
