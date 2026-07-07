using AttendanceEntity = A360.People.Domain.Entities.PersonalVisionManualAttendance;
using MongoDB.Driver;
using A360.Repository.Repositories;

namespace A360.People.Repository.Repositories;

public sealed class PersonalVisionManualAttendanceRepository
    : MongoRepository<AttendanceEntity>,
      IPersonalVisionManualAttendanceRepository,
      IMongoIndexConfigurator
{
    public const string CollectionName = "personalvisionmanualattendance";

    public PersonalVisionManualAttendanceRepository(
        IMongoDatabase database)
        : base(database.GetCollection<AttendanceEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AttendanceEntity>(
                Builders<AttendanceEntity>.IndexKeys
                    .Ascending(x => x.EmployeeId),
                new CreateIndexOptions
                {
                    Name = "ix_manualattendance_employeeid"
                }),

            new CreateIndexModel<AttendanceEntity>(
                Builders<AttendanceEntity>.IndexKeys
                    .Ascending(x => x.ApproveStatus),
                new CreateIndexOptions
                {
                    Name = "ix_manualattendance_approvestatus"
                }),

            new CreateIndexModel<AttendanceEntity>(
                Builders<AttendanceEntity>.IndexKeys
                    .Ascending(x => x.FromDate),
                new CreateIndexOptions
                {
                    Name = "ix_manualattendance_fromdate"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}