using MongoDB.Driver;
using A360.Repository.Repositories;
using OrganizationEntity = A360.MasterManagement.Domain.Entities.Organization;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class OrganizationRepository : MongoRepository<OrganizationEntity>, IOrganizationRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "organizations";

    public OrganizationRepository(IMongoDatabase database)
        : base(database.GetCollection<OrganizationEntity>(CollectionName))
    {
    }

    public async Task<OrganizationEntity?> GetByOrganizationCodeAsync(string organizationCode, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(organization => organization.OrganizationCode == organizationCode)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<OrganizationEntity>(
                Builders<OrganizationEntity>.IndexKeys.Ascending(organization => organization.OrganizationCode),
                new CreateIndexOptions { Name = "ix_organizations_organization_code", Unique = true }),
            new CreateIndexModel<OrganizationEntity>(
                Builders<OrganizationEntity>.IndexKeys.Ascending(organization => organization.AssetId),
                new CreateIndexOptions { Name = "ix_organizations_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
