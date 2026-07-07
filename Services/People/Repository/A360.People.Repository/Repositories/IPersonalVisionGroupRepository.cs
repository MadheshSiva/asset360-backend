using PersonalVisionGroupEntity = A360.People.Domain.Entities.PersonalVisionGroup;
using A360.Repository.Repositories;

namespace A360.People.Repository.Repositories;

public interface IPersonalVisionGroupRepository
    : IMongoRepository<PersonalVisionGroupEntity>
{
}