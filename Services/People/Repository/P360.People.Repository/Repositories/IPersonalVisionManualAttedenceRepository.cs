using AttendanceEntity = P360.People.Domain.Entities.PersonalVisionManualAttendance;
using P360.Repository.Repositories;

namespace P360.People.Repository.Repositories;

public interface IPersonalVisionManualAttendanceRepository
    : IMongoRepository<AttendanceEntity>
{
}