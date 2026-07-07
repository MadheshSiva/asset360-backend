
using DeviceEntity = A360.Devices.Domain.Entities.Device;
using MongoDB.Driver;
using A360.Repository.Repositories;

namespace A360.Devices.Repository.Repositories;

public sealed class DeviceRepository : MongoRepository<DeviceEntity>,
    IDeviceRepository,
    IMongoIndexConfigurator
{
    public const string CollectionName = "device";

    public DeviceRepository(IMongoDatabase database)
        : base(database.GetCollection<DeviceEntity>(CollectionName))
    {
    }

    public async Task<IReadOnlyCollection<DeviceEntity>> GetByTypeAsync(
        string type,
        CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(entity => entity.Type == type)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<DeviceEntity>(
                Builders<DeviceEntity>.IndexKeys
                    .Ascending(x => x.ClientId)
                    .Ascending(x => x.ReferenceId),
                new CreateIndexOptions
                {
                    Name = "ix_device_client_reference"
                }),

            new CreateIndexModel<DeviceEntity>(
                Builders<DeviceEntity>.IndexKeys
                    .Ascending(x => x.ProjectId)
                    .Ascending(x => x.BuildingId)
                    .Ascending(x => x.FloorId),
                new CreateIndexOptions
                {
                    Name = "ix_device_project_building_floor"
                }),

            new CreateIndexModel<DeviceEntity>(
                Builders<DeviceEntity>.IndexKeys
                    .Ascending(x => x.Type)
                    .Ascending(x => x.Technology),
                new CreateIndexOptions
                {
                    Name = "ix_device_type_technology"
                }),

            new CreateIndexModel<DeviceEntity>(
                Builders<DeviceEntity>.IndexKeys
                    .Ascending(x => x.UniqueId),
                new CreateIndexOptions
                {
                    Name = "ix_device_uniqueid",
                    Unique = true
                }),

            new CreateIndexModel<DeviceEntity>(
                Builders<DeviceEntity>.IndexKeys
                    .Ascending(x => x.CountryId)
                    .Ascending(x => x.ZoneId),
                new CreateIndexOptions
                {
                    Name = "ix_device_country_zone"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}
