using A360.Repository.Repositories;
using AssetOwnershipEntity = A360.Asset.Domain.Entities.AssetOwnership;

namespace A360.Asset.Repository.Repositories;

public interface IAssetOwnershipRepository : IMongoRepository<AssetOwnershipEntity>
{
    Task<AssetOwnershipEntity?> GetByOwnershipIdAsync(string ownershipId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<AssetOwnershipEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
}
