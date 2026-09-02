using A360.Repository.Repositories;
using AssetDomainEntity = A360.Asset.Domain.Entities.AssetDomain;

namespace A360.Asset.Repository.Repositories;

public interface IAssetDomainRepository : IMongoRepository<AssetDomainEntity>
{
    Task<AssetDomainEntity?> GetByAssetDomainIdAsync(string assetDomainId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<AssetDomainEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
}
