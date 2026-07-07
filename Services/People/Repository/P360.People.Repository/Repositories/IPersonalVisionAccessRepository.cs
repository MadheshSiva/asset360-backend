using AccessEntity = P360.People.Domain.Entities.PersonalVisionAccess;
using P360.Repository.Repositories;

namespace P360.People.Repository.Repositories;

public interface IPersonalVisionAccessRepository
    : IMongoRepository<AccessEntity>
{
}