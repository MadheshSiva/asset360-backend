using MongoDB.Driver;
using A360.Repository.Repositories;
using SeverityMasterEntity = A360.MasterManagement.Domain.Entities.SeverityMaster;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class SeverityMasterRepository : MongoRepository<SeverityMasterEntity>, ISeverityMasterRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "severity_masters";

    public SeverityMasterRepository(IMongoDatabase database)
        : base(database.GetCollection<SeverityMasterEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<SeverityMasterEntity>(
                Builders<SeverityMasterEntity>.IndexKeys.Ascending(severityMaster => severityMaster.SeverityId),
                new CreateIndexOptions { Name = "ix_severity_masters_severity_id", Unique = true }),
            new CreateIndexModel<SeverityMasterEntity>(
                Builders<SeverityMasterEntity>.IndexKeys.Ascending(severityMaster => severityMaster.AssetId),
                new CreateIndexOptions { Name = "ix_severity_masters_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
