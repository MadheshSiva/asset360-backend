using A360.Repository.Repositories;
using AssetLifecycleEntity = A360.Asset.Domain.Entities.AssetLifecycle;

namespace A360.Asset.Repository.Repositories;

public interface IAssetLifecycleRepository : IMongoRepository<AssetLifecycleEntity>
{
    Task<AssetLifecycleEntity?> GetByLifecycleIdAsync(string lifecycleId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<AssetLifecycleEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
}
