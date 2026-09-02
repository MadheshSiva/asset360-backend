using A360.Repository.Repositories;
using AssetDisposalEntity = A360.Asset.Domain.Entities.AssetDisposal;

namespace A360.Asset.Repository.Repositories;

public interface IAssetDisposalRepository : IMongoRepository<AssetDisposalEntity>
{
    Task<AssetDisposalEntity?> GetByDisposalIdAsync(string disposalId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<AssetDisposalEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
}
