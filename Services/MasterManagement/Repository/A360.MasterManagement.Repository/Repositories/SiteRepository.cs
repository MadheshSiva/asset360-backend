using MongoDB.Driver;
using A360.Repository.Repositories;
using SiteEntity = A360.MasterManagement.Domain.Entities.Site;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class SiteRepository : MongoRepository<SiteEntity>, ISiteRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "sites";

    public SiteRepository(IMongoDatabase database)
        : base(database.GetCollection<SiteEntity>(CollectionName))
    {
    }

    public async Task<SiteEntity?> GetBySiteCodeAsync(string siteCode, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(site => site.SiteCode == siteCode)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<SiteEntity>(
                Builders<SiteEntity>.IndexKeys.Ascending(site => site.SiteCode),
                new CreateIndexOptions { Name = "ix_sites_site_code", Unique = true }),
            new CreateIndexModel<SiteEntity>(
                Builders<SiteEntity>.IndexKeys.Ascending(site => site.AssetId),
                new CreateIndexOptions { Name = "ix_sites_asset_id" }),
            new CreateIndexModel<SiteEntity>(
                Builders<SiteEntity>.IndexKeys.Ascending(site => site.Organization),
                new CreateIndexOptions { Name = "ix_sites_organization" }),
            new CreateIndexModel<SiteEntity>(
                Builders<SiteEntity>.IndexKeys.Ascending(site => site.BusinessUnit),
                new CreateIndexOptions { Name = "ix_sites_business_unit" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
