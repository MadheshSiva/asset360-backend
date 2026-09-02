using A360.Repository.Repositories;
using AssetCheckinEntity = A360.Asset.Domain.Entities.AssetCheckin;

namespace A360.Asset.Repository.Repositories;

public interface IAssetCheckinRepository : IMongoRepository<AssetCheckinEntity>
{
    Task<AssetCheckinEntity?> GetByCheckinIdAsync(string checkinId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<AssetCheckinEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
}
