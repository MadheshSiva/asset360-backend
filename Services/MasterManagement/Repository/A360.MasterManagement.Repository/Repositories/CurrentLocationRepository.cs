using MongoDB.Driver;
using A360.Repository.Repositories;
using CurrentLocationEntity = A360.MasterManagement.Domain.Entities.CurrentLocation;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class CurrentLocationRepository : MongoRepository<CurrentLocationEntity>, ICurrentLocationRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "current_locations";

    public CurrentLocationRepository(IMongoDatabase database)
        : base(database.GetCollection<CurrentLocationEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<CurrentLocationEntity>(
                Builders<CurrentLocationEntity>.IndexKeys.Ascending(currentLocation => currentLocation.LocationId),
                new CreateIndexOptions { Name = "ix_current_locations_location_id", Unique = true }),
            new CreateIndexModel<CurrentLocationEntity>(
                Builders<CurrentLocationEntity>.IndexKeys.Ascending(currentLocation => currentLocation.CurrentLocationName),
                new CreateIndexOptions { Name = "ix_current_locations_current_location_name" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
