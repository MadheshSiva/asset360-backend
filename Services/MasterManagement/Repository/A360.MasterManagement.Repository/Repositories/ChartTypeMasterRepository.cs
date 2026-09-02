using MongoDB.Driver;
using A360.Repository.Repositories;
using ChartTypeMasterEntity = A360.MasterManagement.Domain.Entities.ChartTypeMaster;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class ChartTypeMasterRepository : MongoRepository<ChartTypeMasterEntity>, IChartTypeMasterRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "chart_type_masters";

    public ChartTypeMasterRepository(IMongoDatabase database)
        : base(database.GetCollection<ChartTypeMasterEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<ChartTypeMasterEntity>(
                Builders<ChartTypeMasterEntity>.IndexKeys.Ascending(chartTypeMaster => chartTypeMaster.WidgetId),
                new CreateIndexOptions { Name = "ix_chart_type_masters_widget_id", Unique = true }),
            new CreateIndexModel<ChartTypeMasterEntity>(
                Builders<ChartTypeMasterEntity>.IndexKeys.Ascending(chartTypeMaster => chartTypeMaster.AssetId),
                new CreateIndexOptions { Name = "ix_chart_type_masters_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
