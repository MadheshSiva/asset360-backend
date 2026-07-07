using WorkScheduleEntity = A360.People.Domain.Entities.PersonalWorkSchedule;
using A360.Repository.Repositories;

namespace A360.People.Repository.Repositories;

public interface IPersonalWorkScheduleRepository
    : IMongoRepository<WorkScheduleEntity>
{
}