using MongoDB.Driver;
using A360.Repository.Repositories;
using CertificationTypeMasterEntity = A360.MasterManagement.Domain.Entities.CertificationTypeMaster;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class CertificationTypeMasterRepository : MongoRepository<CertificationTypeMasterEntity>, ICertificationTypeMasterRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "certification_type_masters";

    public CertificationTypeMasterRepository(IMongoDatabase database)
        : base(database.GetCollection<CertificationTypeMasterEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<CertificationTypeMasterEntity>(
                Builders<CertificationTypeMasterEntity>.IndexKeys.Ascending(certificationTypeMaster => certificationTypeMaster.CertificationId),
                new CreateIndexOptions { Name = "ix_certification_type_masters_certification_id", Unique = true }),
            new CreateIndexModel<CertificationTypeMasterEntity>(
                Builders<CertificationTypeMasterEntity>.IndexKeys.Ascending(certificationTypeMaster => certificationTypeMaster.AssetId),
                new CreateIndexOptions { Name = "ix_certification_type_masters_asset_id" }),
            new CreateIndexModel<CertificationTypeMasterEntity>(
                Builders<CertificationTypeMasterEntity>.IndexKeys.Ascending(certificationTypeMaster => certificationTypeMaster.CertificationCode),
                new CreateIndexOptions { Name = "ix_certification_type_masters_certification_code" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
