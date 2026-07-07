using WorkScheduleEntity = A360.People.Domain.Entities.PersonalWorkSchedule;
using MongoDB.Driver;
using A360.Repository.Repositories;

namespace A360.People.Repository.Repositories;

public sealed class PersonalWorkScheduleRepository
    : MongoRepository<WorkScheduleEntity>,
      IPersonalWorkScheduleRepository,
      IMongoIndexConfigurator
{
    public const string CollectionName = "personalworkschedule";

    public PersonalWorkScheduleRepository(
        IMongoDatabase database)
        : base(database.GetCollection<WorkScheduleEntity>(
            CollectionName))
    {
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<WorkScheduleEntity>(
                Builders<WorkScheduleEntity>.IndexKeys
                    .Ascending(x => x.GroupId),
                new CreateIndexOptions
                {
                    Name = "ix_personalworkschedule_groupid"
                }),

            new CreateIndexModel<WorkScheduleEntity>(
                Builders<WorkScheduleEntity>.IndexKeys
                    .Ascending(x => x.WorkScheduleName),
                new CreateIndexOptions
                {
                    Name = "ix_personalworkschedule_name"
                }),

            new CreateIndexModel<WorkScheduleEntity>(
                Builders<WorkScheduleEntity>.IndexKeys
                    .Ascending(x => x.ScheduleType),
                new CreateIndexOptions
                {
                    Name = "ix_personalworkschedule_scheduletype"
                }),

            new CreateIndexModel<WorkScheduleEntity>(
                Builders<WorkScheduleEntity>.IndexKeys
                    .Ascending(x => x.Status),
                new CreateIndexOptions
                {
                    Name = "ix_personalworkschedule_status"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}