using MongoDB.Driver;
using A360.Repository.Repositories;
using ZoneMappingEntity = A360.Project.Domain.Entities.ZoneMapping;

namespace A360.Project.Repository.Repositories;

public sealed class ZoneMappingRepository : MongoRepository<ZoneMappingEntity>, IZoneMappingRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "zonemappingdata";

    public ZoneMappingRepository(IMongoDatabase database)
        : base(database.GetCollection<ZoneMappingEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<ZoneMappingEntity>(
                Builders<ZoneMappingEntity>.IndexKeys
                    .Ascending(zoneMapping => zoneMapping.ProjectId)
                    .Ascending(zoneMapping => zoneMapping.CountryId)
                    .Ascending(zoneMapping => zoneMapping.AreaId)
                    .Ascending(zoneMapping => zoneMapping.BuildingId)
                    .Ascending(zoneMapping => zoneMapping.FloorId),
                new CreateIndexOptions { Name = "ix_zonemapping_project_country_area_building_floor" }),
            new CreateIndexModel<ZoneMappingEntity>(
                Builders<ZoneMappingEntity>.IndexKeys.Ascending(zoneMapping => zoneMapping.ZoneId),
                new CreateIndexOptions { Name = "ix_zonemapping_zone_id" }),
            new CreateIndexModel<ZoneMappingEntity>(
                Builders<ZoneMappingEntity>.IndexKeys
                    .Ascending(zoneMapping => zoneMapping.ClientId)
                    .Ascending(zoneMapping => zoneMapping.Status),
                new CreateIndexOptions { Name = "ix_zonemapping_client_status" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
