using PersonalVisionGroupEntity = P360.People.Domain.Entities.PersonalVisionGroup;
using P360.Repository.Repositories;

namespace P360.People.Repository.Repositories;

public interface IPersonalVisionGroupRepository
    : IMongoRepository<PersonalVisionGroupEntity>
{
}