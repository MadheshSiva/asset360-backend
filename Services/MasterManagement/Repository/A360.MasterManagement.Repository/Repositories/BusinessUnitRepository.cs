using MongoDB.Driver;
using A360.Repository.Repositories;
using BusinessUnitEntity = A360.MasterManagement.Domain.Entities.BusinessUnit;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class BusinessUnitRepository : MongoRepository<BusinessUnitEntity>, IBusinessUnitRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "business_units";

    public BusinessUnitRepository(IMongoDatabase database)
        : base(database.GetCollection<BusinessUnitEntity>(CollectionName))
    {
    }

    public async Task<BusinessUnitEntity?> GetByBusinessUnitCodeAsync(string businessUnitCode, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(businessUnit => businessUnit.BusinessUnitCode == businessUnitCode)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<BusinessUnitEntity>(
                Builders<BusinessUnitEntity>.IndexKeys.Ascending(businessUnit => businessUnit.BusinessUnitCode),
                new CreateIndexOptions { Name = "ix_business_units_business_unit_code", Unique = true }),
            new CreateIndexModel<BusinessUnitEntity>(
                Builders<BusinessUnitEntity>.IndexKeys.Ascending(businessUnit => businessUnit.AssetId),
                new CreateIndexOptions { Name = "ix_business_units_asset_id" }),
            new CreateIndexModel<BusinessUnitEntity>(
                Builders<BusinessUnitEntity>.IndexKeys.Ascending(businessUnit => businessUnit.Organization),
                new CreateIndexOptions { Name = "ix_business_units_organization" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
