using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetIncidentEntity = A360.Asset.Domain.Entities.AssetIncident;

namespace A360.Asset.Repository.Repositories;

public sealed class AssetIncidentRepository : MongoRepository<AssetIncidentEntity>, IAssetIncidentRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "asset_incidents";

    public AssetIncidentRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetIncidentEntity>(CollectionName))
    {
    }

    public async Task<AssetIncidentEntity?> GetByIncidentIdAsync(string incidentId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(incident => incident.IncidentId == incidentId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<AssetIncidentEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(incident => incident.AssetId == assetId)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetIncidentEntity>(
                Builders<AssetIncidentEntity>.IndexKeys.Ascending(incident => incident.IncidentId),
                new CreateIndexOptions { Name = "ix_asset_incidents_incident_id", Unique = true }),
            new CreateIndexModel<AssetIncidentEntity>(
                Builders<AssetIncidentEntity>.IndexKeys.Ascending(incident => incident.AssetId),
                new CreateIndexOptions { Name = "ix_asset_incidents_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
