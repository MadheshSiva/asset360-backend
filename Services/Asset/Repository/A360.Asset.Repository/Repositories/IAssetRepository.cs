using A360.Repository.Repositories;
using AssetEntity = A360.Asset.Domain.Entities.Asset;

namespace A360.Asset.Repository.Repositories;

public interface IAssetRepository : IMongoRepository<AssetEntity>
{
    Task<AssetEntity?> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
}
