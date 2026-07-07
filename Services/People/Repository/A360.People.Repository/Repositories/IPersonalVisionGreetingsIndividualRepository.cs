using GreetingsEntity = A360.People.Domain.Entities.PersonalVisionGreetingsIndividual;
using A360.Repository.Repositories;

namespace A360.People.Repository.Repositories;

public interface IPersonalVisionGreetingsIndividualRepository
    : IMongoRepository<GreetingsEntity>
{
}