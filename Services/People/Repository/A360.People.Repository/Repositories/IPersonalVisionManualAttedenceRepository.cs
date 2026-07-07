using AttendanceEntity = A360.People.Domain.Entities.PersonalVisionManualAttendance;
using A360.Repository.Repositories;

namespace A360.People.Repository.Repositories;

public interface IPersonalVisionManualAttendanceRepository
    : IMongoRepository<AttendanceEntity>
{
}