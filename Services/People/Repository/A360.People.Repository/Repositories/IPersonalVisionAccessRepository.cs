using AccessEntity = A360.People.Domain.Entities.PersonalVisionAccess;
using A360.Repository.Repositories;

namespace A360.People.Repository.Repositories;

public interface IPersonalVisionAccessRepository
    : IMongoRepository<AccessEntity>
{
}