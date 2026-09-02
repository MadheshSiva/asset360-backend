using MongoDB.Driver;
using A360.Repository.Repositories;
using AuditorDetailEntity = A360.MasterManagement.Domain.Entities.AuditorDetail;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class AuditorDetailRepository : MongoRepository<AuditorDetailEntity>, IAuditorDetailRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "auditor_details";

    public AuditorDetailRepository(IMongoDatabase database)
        : base(database.GetCollection<AuditorDetailEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AuditorDetailEntity>(
                Builders<AuditorDetailEntity>.IndexKeys.Ascending(auditorDetail => auditorDetail.AuditorId),
                new CreateIndexOptions { Name = "ix_auditor_details_auditor_id", Unique = true }),
            new CreateIndexModel<AuditorDetailEntity>(
                Builders<AuditorDetailEntity>.IndexKeys.Ascending(auditorDetail => auditorDetail.AssetId),
                new CreateIndexOptions { Name = "ix_auditor_details_asset_id" }),
            new CreateIndexModel<AuditorDetailEntity>(
                Builders<AuditorDetailEntity>.IndexKeys.Ascending(auditorDetail => auditorDetail.EmployeeCode),
                new CreateIndexOptions { Name = "ix_auditor_details_employee_code" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
