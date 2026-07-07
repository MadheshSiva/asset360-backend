
using EvacuationEntity = A360.Evacuation.Domain.Entities.Evacuation;
using MongoDB.Driver;
using A360.Repository.Repositories;

namespace A360.Evacuation.Repository.Repositories;

public sealed class EvacuationRepository : MongoRepository<EvacuationEntity>,
    IEvacuationRepository,
    IMongoIndexConfigurator
{
    public const string CollectionName = "evacuation";

    public EvacuationRepository(IMongoDatabase database)
        : base(database.GetCollection<EvacuationEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<EvacuationEntity>(
                Builders<EvacuationEntity>.IndexKeys
                    .Ascending(x => x.ClientId)
                    .Ascending(x => x.ReferenceId),
                new CreateIndexOptions
                {
                    Name = "ix_evacuation_client_reference"
                }),

            new CreateIndexModel<EvacuationEntity>(
                Builders<EvacuationEntity>.IndexKeys
                    .Ascending(x => x.ProjectId)
                    .Ascending(x => x.BuildingId)
                    .Ascending(x => x.FloorId),
                new CreateIndexOptions
                {
                    Name = "ix_evacuation_project_building_floor"
                }),

            new CreateIndexModel<EvacuationEntity>(
                Builders<EvacuationEntity>.IndexKeys
                    .Ascending(x => x.CountryId)
                    .Ascending(x => x.AreaId),
                new CreateIndexOptions
                {
                    Name = "ix_evacuation_country_area"
                }),

            new CreateIndexModel<EvacuationEntity>(
                Builders<EvacuationEntity>.IndexKeys
                    .Ascending(x => x.ZoneId),
                new CreateIndexOptions
                {
                    Name = "ix_evacuation_zone"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}
