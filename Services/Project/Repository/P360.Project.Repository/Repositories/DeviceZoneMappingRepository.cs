using MongoDB.Driver;
using P360.Repository.Repositories;
using DeviceZoneMappingEntity = P360.Project.Domain.Entities.DeviceZoneMapping;

namespace P360.Project.Repository.Repositories;

public sealed class DeviceZoneMappingRepository : MongoRepository<DeviceZoneMappingEntity>, IDeviceZoneMappingRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "devicezonemappingdata";

    public DeviceZoneMappingRepository(IMongoDatabase database)
        : base(database.GetCollection<DeviceZoneMappingEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<DeviceZoneMappingEntity>(
                Builders<DeviceZoneMappingEntity>.IndexKeys
                    .Ascending(deviceZoneMapping => deviceZoneMapping.ProjectId)
                    .Ascending(deviceZoneMapping => deviceZoneMapping.CountryId)
                    .Ascending(deviceZoneMapping => deviceZoneMapping.AreaId)
                    .Ascending(deviceZoneMapping => deviceZoneMapping.BuildingId)
                    .Ascending(deviceZoneMapping => deviceZoneMapping.FloorId),
                new CreateIndexOptions { Name = "ix_devicezonemapping_project_country_area_building_floor" }),
            new CreateIndexModel<DeviceZoneMappingEntity>(
                Builders<DeviceZoneMappingEntity>.IndexKeys.Ascending(deviceZoneMapping => deviceZoneMapping.ZoneId),
                new CreateIndexOptions { Name = "ix_devicezonemapping_zone_id" }),
            new CreateIndexModel<DeviceZoneMappingEntity>(
                Builders<DeviceZoneMappingEntity>.IndexKeys.Ascending(deviceZoneMapping => deviceZoneMapping.DeviceReferenceId),
                new CreateIndexOptions { Name = "ix_devicezonemapping_device_reference_id" }),
            new CreateIndexModel<DeviceZoneMappingEntity>(
                Builders<DeviceZoneMappingEntity>.IndexKeys
                    .Ascending(deviceZoneMapping => deviceZoneMapping.ClientId)
                    .Ascending(deviceZoneMapping => deviceZoneMapping.Status),
                new CreateIndexOptions { Name = "ix_devicezonemapping_client_status" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
