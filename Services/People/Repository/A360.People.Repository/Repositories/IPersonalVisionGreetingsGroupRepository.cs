using GreetingsGroupsEntity =
    A360.People.Domain.Entities.PersonalVisionGreetingsGroups;

using A360.Repository.Repositories;

namespace A360.People.Repository.Repositories;

public interface IPersonalVisionGreetingsGroupsRepository
    : IMongoRepository<GreetingsGroupsEntity>
{
}