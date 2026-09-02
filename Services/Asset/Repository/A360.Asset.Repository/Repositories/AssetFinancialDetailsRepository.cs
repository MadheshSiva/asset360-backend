using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetFinancialDetailsEntity = A360.Asset.Domain.Entities.AssetFinancialDetails;

namespace A360.Asset.Repository.Repositories;

public sealed class AssetFinancialDetailsRepository : MongoRepository<AssetFinancialDetailsEntity>, IAssetFinancialDetailsRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "asset_financial_details";

    public AssetFinancialDetailsRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetFinancialDetailsEntity>(CollectionName))
    {
    }

    public async Task<AssetFinancialDetailsEntity?> GetByFinancialDetailsIdAsync(string financialDetailsId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(record => record.FinancialDetailsId == financialDetailsId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<AssetFinancialDetailsEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(record => record.AssetId == assetId)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetFinancialDetailsEntity>(
                Builders<AssetFinancialDetailsEntity>.IndexKeys.Ascending(record => record.FinancialDetailsId),
                new CreateIndexOptions { Name = "ix_asset_financial_details_financial_details_id", Unique = true }),
            new CreateIndexModel<AssetFinancialDetailsEntity>(
                Builders<AssetFinancialDetailsEntity>.IndexKeys.Ascending(record => record.AssetId),
                new CreateIndexOptions { Name = "ix_asset_financial_details_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
