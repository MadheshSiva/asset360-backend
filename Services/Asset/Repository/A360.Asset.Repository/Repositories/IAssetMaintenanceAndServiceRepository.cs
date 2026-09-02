using A360.Repository.Repositories;
using AssetMaintenanceAndServiceEntity = A360.Asset.Domain.Entities.AssetMaintenanceAndService;

namespace A360.Asset.Repository.Repositories;

public interface IAssetMaintenanceAndServiceRepository : IMongoRepository<AssetMaintenanceAndServiceEntity>
{
    Task<AssetMaintenanceAndServiceEntity?> GetByMaintenanceServiceIdAsync(string maintenanceServiceId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<AssetMaintenanceAndServiceEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
}
