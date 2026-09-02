using FloorEntity = A360.Project.Domain.Entities.Floor;
using MongoDB.Driver;
using A360.Repository.Repositories;

namespace A360.Project.Repository.Repositories;

public sealed class FloorRepository : MongoRepository<FloorEntity>, IFloorRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "floor";

    public FloorRepository(IMongoDatabase database)
        : base(database.GetCollection<FloorEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<FloorEntity>(
                Builders<FloorEntity>.IndexKeys
                    .Ascending(floor => floor.ProjectId)
                    .Ascending(floor => floor.CountryId)
                    .Ascending(floor => floor.AreaId)
                    .Ascending(floor => floor.OuterZoneId)
                    .Ascending(floor => floor.BuildingId),
                new CreateIndexOptions { Name = "ix_floor_project_country_area_outerzone_building" }),
            new CreateIndexModel<FloorEntity>(
                Builders<FloorEntity>.IndexKeys
                    .Ascending(floor => floor.BuildingId)
                    .Ascending(floor => floor.FloorName),
                new CreateIndexOptions { Name = "ix_floor_building_floor_name" }),
            new CreateIndexModel<FloorEntity>(
                Builders<FloorEntity>.IndexKeys
                    .Ascending(floor => floor.ClientId)
                    .Ascending(floor => floor.Status),
                new CreateIndexOptions { Name = "ix_floor_client_status" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
