using A360.Repository.Repositories;
using AssetContractEntity = A360.Asset.Domain.Entities.AssetContract;

namespace A360.Asset.Repository.Repositories;

public interface IAssetContractRepository : IMongoRepository<AssetContractEntity>
{
    Task<AssetContractEntity?> GetByContractIdAsync(string contractId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<AssetContractEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
}
