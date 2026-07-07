using BuildingEntity = P360.Project.Domain.Entities.Building;
using MongoDB.Driver;
using P360.Repository.Repositories;

namespace P360.Project.Repository.Repositories;

public sealed class BuildingRepository : MongoRepository<BuildingEntity>, IBuildingRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "building";

    public BuildingRepository(IMongoDatabase database)
        : base(database.GetCollection<BuildingEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<BuildingEntity>(
                Builders<BuildingEntity>.IndexKeys
                    .Ascending(building => building.ProjectId)
                    .Ascending(building => building.CountryId)
                    .Ascending(building => building.AreaId),
                new CreateIndexOptions { Name = "ix_building_project_country_area" }),
            new CreateIndexModel<BuildingEntity>(
                Builders<BuildingEntity>.IndexKeys
                    .Ascending(building => building.AreaId)
                    .Ascending(building => building.BuildingName),
                new CreateIndexOptions { Name = "ix_building_area_building_name" }),
            new CreateIndexModel<BuildingEntity>(
                Builders<BuildingEntity>.IndexKeys
                    .Ascending(building => building.ClientId)
                    .Ascending(building => building.Status),
                new CreateIndexOptions { Name = "ix_building_client_status" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
