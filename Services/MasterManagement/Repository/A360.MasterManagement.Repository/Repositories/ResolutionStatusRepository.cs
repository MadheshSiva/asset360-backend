using MongoDB.Driver;
using A360.Repository.Repositories;
using ResolutionStatusEntity = A360.MasterManagement.Domain.Entities.ResolutionStatus;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class ResolutionStatusRepository : MongoRepository<ResolutionStatusEntity>, IResolutionStatusRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "resolution_statuses";

    public ResolutionStatusRepository(IMongoDatabase database)
        : base(database.GetCollection<ResolutionStatusEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<ResolutionStatusEntity>(
                Builders<ResolutionStatusEntity>.IndexKeys.Ascending(resolutionStatus => resolutionStatus.StatusId),
                new CreateIndexOptions { Name = "ix_resolution_statuses_status_id", Unique = true }),
            new CreateIndexModel<ResolutionStatusEntity>(
                Builders<ResolutionStatusEntity>.IndexKeys.Ascending(resolutionStatus => resolutionStatus.AssetId),
                new CreateIndexOptions { Name = "ix_resolution_statuses_asset_id" }),
            new CreateIndexModel<ResolutionStatusEntity>(
                Builders<ResolutionStatusEntity>.IndexKeys.Ascending(resolutionStatus => resolutionStatus.StatusCode),
                new CreateIndexOptions { Name = "ix_resolution_statuses_status_code" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
