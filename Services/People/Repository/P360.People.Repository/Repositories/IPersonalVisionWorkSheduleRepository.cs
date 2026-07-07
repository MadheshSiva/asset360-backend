using WorkScheduleEntity = P360.People.Domain.Entities.PersonalWorkSchedule;
using P360.Repository.Repositories;

namespace P360.People.Repository.Repositories;

public interface IPersonalWorkScheduleRepository
    : IMongoRepository<WorkScheduleEntity>
{
}