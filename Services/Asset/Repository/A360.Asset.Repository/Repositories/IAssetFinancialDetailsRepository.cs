using A360.Repository.Repositories;
using AssetFinancialDetailsEntity = A360.Asset.Domain.Entities.AssetFinancialDetails;

namespace A360.Asset.Repository.Repositories;

public interface IAssetFinancialDetailsRepository : IMongoRepository<AssetFinancialDetailsEntity>
{
    Task<AssetFinancialDetailsEntity?> GetByFinancialDetailsIdAsync(string financialDetailsId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<AssetFinancialDetailsEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
}
