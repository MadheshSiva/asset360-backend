using A360.Repository.Repositories;
using AssetAuditAndVerificationEntity = A360.Asset.Domain.Entities.AssetAuditAndVerification;

namespace A360.Asset.Repository.Repositories;

public interface IAssetAuditAndVerificationRepository : IMongoRepository<AssetAuditAndVerificationEntity>
{
    Task<AssetAuditAndVerificationEntity?> GetByAuditVerificationIdAsync(string auditVerificationId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<AssetAuditAndVerificationEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
}
