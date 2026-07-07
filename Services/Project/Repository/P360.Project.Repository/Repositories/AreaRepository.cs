using AreaEntity = P360.Project.Domain.Entities.Area;
using MongoDB.Driver;
using P360.Repository.Repositories;

namespace P360.Project.Repository.Repositories;

public sealed class AreaRepository : MongoRepository<AreaEntity>, IAreaRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "area";

    public AreaRepository(IMongoDatabase database)
        : base(database.GetCollection<AreaEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AreaEntity>(
                Builders<AreaEntity>.IndexKeys
                    .Ascending(area => area.ProjectId)
                    .Ascending(area => area.CountryId),
                new CreateIndexOptions { Name = "ix_area_project_country" }),
            new CreateIndexModel<AreaEntity>(
                Builders<AreaEntity>.IndexKeys
                    .Ascending(area => area.CountryId)
                    .Ascending(area => area.AreaName),
                new CreateIndexOptions { Name = "ix_area_country_area_name" }),
            new CreateIndexModel<AreaEntity>(
                Builders<AreaEntity>.IndexKeys
                    .Ascending(area => area.ClientId)
                    .Ascending(area => area.Status),
                new CreateIndexOptions { Name = "ix_area_client_status" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
