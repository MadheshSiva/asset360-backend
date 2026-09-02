using MongoDB.Driver;
using A360.Repository.Repositories;
using AlertTypeEntity = A360.MasterManagement.Domain.Entities.AlertType;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class AlertTypeRepository : MongoRepository<AlertTypeEntity>, IAlertTypeRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "alert_types";

    public AlertTypeRepository(IMongoDatabase database)
        : base(database.GetCollection<AlertTypeEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AlertTypeEntity>(
                Builders<AlertTypeEntity>.IndexKeys.Ascending(alertType => alertType.AlertTypeId),
                new CreateIndexOptions { Name = "ix_alert_types_alert_type_id", Unique = true }),
            new CreateIndexModel<AlertTypeEntity>(
                Builders<AlertTypeEntity>.IndexKeys.Ascending(alertType => alertType.AssetId),
                new CreateIndexOptions { Name = "ix_alert_types_asset_id" }),
            new CreateIndexModel<AlertTypeEntity>(
                Builders<AlertTypeEntity>.IndexKeys.Ascending(alertType => alertType.AlertCode),
                new CreateIndexOptions { Name = "ix_alert_types_alert_code" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
