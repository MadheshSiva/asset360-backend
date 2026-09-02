using A360.Repository.Repositories;
using AssetCertificationEntity = A360.Asset.Domain.Entities.AssetCertification;

namespace A360.Asset.Repository.Repositories;

public interface IAssetCertificationRepository : IMongoRepository<AssetCertificationEntity>
{
    Task<AssetCertificationEntity?> GetByCertificationIdAsync(string certificationId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<AssetCertificationEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
}
