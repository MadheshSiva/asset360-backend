using A360.Repository.Repositories;
using AssetMovementEntity = A360.Asset.Domain.Entities.AssetMovement;

namespace A360.Asset.Repository.Repositories;

public interface IAssetMovementRepository : IMongoRepository<AssetMovementEntity>
{
    Task<AssetMovementEntity?> GetByMovementIdAsync(string movementId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<AssetMovementEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
}
