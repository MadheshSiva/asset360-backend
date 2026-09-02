using A360.Repository.Repositories;
using AssetIncidentEntity = A360.Asset.Domain.Entities.AssetIncident;

namespace A360.Asset.Repository.Repositories;

public interface IAssetIncidentRepository : IMongoRepository<AssetIncidentEntity>
{
    Task<AssetIncidentEntity?> GetByIncidentIdAsync(string incidentId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<AssetIncidentEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
}
