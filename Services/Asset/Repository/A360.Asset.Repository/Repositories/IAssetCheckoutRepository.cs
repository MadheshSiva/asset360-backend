using A360.Repository.Repositories;
using AssetCheckoutEntity = A360.Asset.Domain.Entities.AssetCheckout;

namespace A360.Asset.Repository.Repositories;

public interface IAssetCheckoutRepository : IMongoRepository<AssetCheckoutEntity>
{
    Task<AssetCheckoutEntity?> GetByCheckoutIdAsync(string checkoutId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<AssetCheckoutEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
}
