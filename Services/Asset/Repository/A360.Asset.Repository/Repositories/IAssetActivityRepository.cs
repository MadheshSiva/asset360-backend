using A360.Repository.Repositories;
using AssetActivityEntity = A360.Asset.Domain.Entities.AssetActivity;

namespace A360.Asset.Repository.Repositories;

public interface IAssetActivityRepository : IMongoRepository<AssetActivityEntity>
{
    Task<AssetActivityEntity?> GetByActivityIdAsync(string activityId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<AssetActivityEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
}
