using A360.Repository.Repositories;
using AssetTrackingAndTelemetryEntity = A360.Asset.Domain.Entities.AssetTrackingAndTelemetry;

namespace A360.Asset.Repository.Repositories;

public interface IAssetTrackingAndTelemetryRepository : IMongoRepository<AssetTrackingAndTelemetryEntity>
{
    Task<AssetTrackingAndTelemetryEntity?> GetByTrackingIdAsync(string trackingId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<AssetTrackingAndTelemetryEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
}
