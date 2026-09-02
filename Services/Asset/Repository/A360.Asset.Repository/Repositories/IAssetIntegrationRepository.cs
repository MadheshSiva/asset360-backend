using A360.Repository.Repositories;
using AssetIntegrationEntity = A360.Asset.Domain.Entities.AssetIntegration;

namespace A360.Asset.Repository.Repositories;

public interface IAssetIntegrationRepository : IMongoRepository<AssetIntegrationEntity>
{
    Task<AssetIntegrationEntity?> GetByIntegrationIdAsync(string integrationId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<AssetIntegrationEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
}
