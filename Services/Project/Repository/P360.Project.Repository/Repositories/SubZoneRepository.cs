using MongoDB.Driver;
using P360.Repository.Repositories;
using SubZoneEntity = P360.Project.Domain.Entities.SubZone;

namespace P360.Project.Repository.Repositories;

public sealed class SubZoneRepository : MongoRepository<SubZoneEntity>, ISubZoneRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "subzone";

    public SubZoneRepository(IMongoDatabase database)
        : base(database.GetCollection<SubZoneEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<SubZoneEntity>(
                Builders<SubZoneEntity>.IndexKeys
                    .Ascending(subZone => subZone.ProjectId)
                    .Ascending(subZone => subZone.CountryId)
                    .Ascending(subZone => subZone.AreaId)
                    .Ascending(subZone => subZone.BuildingId)
                    .Ascending(subZone => subZone.FloorId)
                    .Ascending(subZone => subZone.ZoneId),
                new CreateIndexOptions { Name = "ix_subzone_project_country_area_building_floor_zone" }),
            new CreateIndexModel<SubZoneEntity>(
                Builders<SubZoneEntity>.IndexKeys
                    .Ascending(subZone => subZone.ZoneId)
                    .Ascending(subZone => subZone.SubZoneName),
                new CreateIndexOptions { Name = "ix_subzone_zone_subzone_name" }),
            new CreateIndexModel<SubZoneEntity>(
                Builders<SubZoneEntity>.IndexKeys
                    .Ascending(subZone => subZone.ClientId)
                    .Ascending(subZone => subZone.Status),
                new CreateIndexOptions { Name = "ix_subzone_client_status" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
