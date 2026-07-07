using GreetingsEntity = P360.People.Domain.Entities.PersonalVisionGreetingsIndividual;
using P360.Repository.Repositories;

namespace P360.People.Repository.Repositories;

public interface IPersonalVisionGreetingsIndividualRepository
    : IMongoRepository<GreetingsEntity>
{
}