using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetIntegrationEntity = A360.Asset.Domain.Entities.AssetIntegration;

namespace A360.Asset.Repository.Repositories;

public sealed class AssetIntegrationRepository : MongoRepository<AssetIntegrationEntity>, IAssetIntegrationRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "asset_integrations";

    public AssetIntegrationRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetIntegrationEntity>(CollectionName))
    {
    }

    public async Task<AssetIntegrationEntity?> GetByIntegrationIdAsync(string integrationId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(integration => integration.IntegrationId == integrationId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<AssetIntegrationEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(integration => integration.AssetId == assetId)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetIntegrationEntity>(
                Builders<AssetIntegrationEntity>.IndexKeys.Ascending(integration => integration.IntegrationId),
                new CreateIndexOptions { Name = "ix_asset_integrations_integration_id", Unique = true }),
            new CreateIndexModel<AssetIntegrationEntity>(
                Builders<AssetIntegrationEntity>.IndexKeys.Ascending(integration => integration.AssetId),
                new CreateIndexOptions { Name = "ix_asset_integrations_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
