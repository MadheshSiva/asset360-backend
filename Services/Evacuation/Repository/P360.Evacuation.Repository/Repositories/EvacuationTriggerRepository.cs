
using EvacuationTriggerEntity = P360.Evacuation.Domain.Entities.EvacuationTrigger;
using MongoDB.Driver;
using P360.Repository.Repositories;

namespace P360.Evacuation.Repository.Repositories;

public sealed class EvacuationTriggerRepository : MongoRepository<EvacuationTriggerEntity>,
    IEvacuationTriggerRepository,
    IMongoIndexConfigurator
{
    public const string CollectionName = "evacuation_trigger";

    public EvacuationTriggerRepository(IMongoDatabase database)
        : base(database.GetCollection<EvacuationTriggerEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<EvacuationTriggerEntity>(
                Builders<EvacuationTriggerEntity>.IndexKeys
                    .Ascending(x => x.ClientId)
                    .Ascending(x => x.ReferenceId),
                new CreateIndexOptions
                {
                    Name = "ix_evacuationtrigger_client_reference"
                }),

            new CreateIndexModel<EvacuationTriggerEntity>(
                Builders<EvacuationTriggerEntity>.IndexKeys
                    .Ascending(x => x.ApplicationName),
                new CreateIndexOptions
                {
                    Name = "ix_evacuationtrigger_application_name"
                }),

            new CreateIndexModel<EvacuationTriggerEntity>(
                Builders<EvacuationTriggerEntity>.IndexKeys
                    .Ascending(x => x.IpAddress),
                new CreateIndexOptions
                {
                    Name = "ix_evacuationtrigger_ip_address"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}
