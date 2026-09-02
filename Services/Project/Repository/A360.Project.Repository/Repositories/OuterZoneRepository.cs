using OuterZoneEntity = A360.Project.Domain.Entities.OuterZone;
using MongoDB.Driver;
using A360.Repository.Repositories;

namespace A360.Project.Repository.Repositories;

public sealed class OuterZoneRepository : MongoRepository<OuterZoneEntity>, IOuterZoneRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "outer_zone";

    public OuterZoneRepository(IMongoDatabase database)
        : base(database.GetCollection<OuterZoneEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<OuterZoneEntity>(
                Builders<OuterZoneEntity>.IndexKeys
                    .Ascending(outerZone => outerZone.ProjectId)
                    .Ascending(outerZone => outerZone.CountryId)
                    .Ascending(outerZone => outerZone.AreaId),
                new CreateIndexOptions { Name = "ix_outerzone_project_country_area" }),
            new CreateIndexModel<OuterZoneEntity>(
                Builders<OuterZoneEntity>.IndexKeys
                    .Ascending(outerZone => outerZone.AreaId)
                    .Ascending(outerZone => outerZone.OuterZoneName),
                new CreateIndexOptions { Name = "ix_outerzone_area_outer_zone_name" }),
            new CreateIndexModel<OuterZoneEntity>(
                Builders<OuterZoneEntity>.IndexKeys
                    .Ascending(outerZone => outerZone.ClientId)
                    .Ascending(outerZone => outerZone.Status),
                new CreateIndexOptions { Name = "ix_outerzone_client_status" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
