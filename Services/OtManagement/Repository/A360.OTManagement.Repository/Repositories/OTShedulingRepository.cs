using OTSchedulingEntity =
    A360.OTManagement.Domain.Entities.OTScheduling;

using MongoDB.Driver;
using A360.Repository.Repositories;

namespace A360.OTManagement.Repository.Repositories;

public sealed class OTSchedulingRepository
    : MongoRepository<OTSchedulingEntity>,
      IOTSchedulingRepository,
      IMongoIndexConfigurator
{
    public const string CollectionName =
        "otscheduling";

    public OTSchedulingRepository(
        IMongoDatabase database)
        : base(
            database.GetCollection<OTSchedulingEntity>(
                CollectionName))
    {
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<OTSchedulingEntity>(
                Builders<OTSchedulingEntity>.IndexKeys
                    .Ascending(x => x.ScheduleId),
                new CreateIndexOptions
                {
                    Name = "ix_otscheduling_scheduleid",
                    Unique = true
                }),

            new CreateIndexModel<OTSchedulingEntity>(
                Builders<OTSchedulingEntity>.IndexKeys
                    .Ascending(x => x.ResourceId),
                new CreateIndexOptions
                {
                    Name = "ix_otscheduling_resourceid"
                }),

            new CreateIndexModel<OTSchedulingEntity>(
                Builders<OTSchedulingEntity>.IndexKeys
                    .Ascending(x => x.Surgeon),
                new CreateIndexOptions
                {
                    Name = "ix_otscheduling_surgeon"
                }),

            new CreateIndexModel<OTSchedulingEntity>(
                Builders<OTSchedulingEntity>.IndexKeys
                    .Ascending(x => x.StartTime)
                    .Ascending(x => x.EndTime),
                new CreateIndexOptions
                {
                    Name = "ix_otscheduling_time"
                }),

            new CreateIndexModel<OTSchedulingEntity>(
                Builders<OTSchedulingEntity>.IndexKeys
                    .Ascending(x => x.SurgeryType)
                    .Ascending(x => x.Priority),
                new CreateIndexOptions
                {
                    Name = "ix_otscheduling_surgery_priority"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}