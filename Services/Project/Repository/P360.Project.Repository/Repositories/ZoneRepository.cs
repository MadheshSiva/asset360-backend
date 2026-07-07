using MongoDB.Driver;
using P360.Repository.Repositories;
using ZoneEntity = P360.Project.Domain.Entities.Zone;

namespace P360.Project.Repository.Repositories;

public sealed class ZoneRepository : MongoRepository<ZoneEntity>, IZoneRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "zone";

    public ZoneRepository(IMongoDatabase database)
        : base(database.GetCollection<ZoneEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<ZoneEntity>(
                Builders<ZoneEntity>.IndexKeys
                    .Ascending(zone => zone.ProjectId)
                    .Ascending(zone => zone.CountryId)
                    .Ascending(zone => zone.AreaId)
                    .Ascending(zone => zone.BuildingId)
                    .Ascending(zone => zone.FloorId),
                new CreateIndexOptions { Name = "ix_zone_project_country_area_building_floor" }),
            new CreateIndexModel<ZoneEntity>(
                Builders<ZoneEntity>.IndexKeys
                    .Ascending(zone => zone.FloorId)
                    .Ascending(zone => zone.ZoneName),
                new CreateIndexOptions { Name = "ix_zone_floor_zone_name" }),
            new CreateIndexModel<ZoneEntity>(
                Builders<ZoneEntity>.IndexKeys
                    .Ascending(zone => zone.ClientId)
                    .Ascending(zone => zone.Status),
                new CreateIndexOptions { Name = "ix_zone_client_status" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
