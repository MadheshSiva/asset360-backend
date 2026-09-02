using A360.Repository.Repositories;
using AssetUtilizationAndPerformanceEntity = A360.Asset.Domain.Entities.AssetUtilizationAndPerformance;

namespace A360.Asset.Repository.Repositories;

public interface IAssetUtilizationAndPerformanceRepository : IMongoRepository<AssetUtilizationAndPerformanceEntity>
{
    Task<AssetUtilizationAndPerformanceEntity?> GetByUtilizationPerformanceIdAsync(string utilizationPerformanceId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<AssetUtilizationAndPerformanceEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
}
