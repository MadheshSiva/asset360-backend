using GreetingsGroupsEntity =
    P360.People.Domain.Entities.PersonalVisionGreetingsGroups;

using P360.Repository.Repositories;

namespace P360.People.Repository.Repositories;

public interface IPersonalVisionGreetingsGroupsRepository
    : IMongoRepository<GreetingsGroupsEntity>
{
}