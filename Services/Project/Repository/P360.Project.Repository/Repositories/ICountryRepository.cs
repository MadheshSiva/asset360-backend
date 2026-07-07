using P360.Repository.Repositories;
using CountryEntity = P360.Project.Domain.Entities.Country;

namespace P360.Project.Repository.Repositories;

public interface ICountryRepository : IMongoRepository<CountryEntity>
{
}
