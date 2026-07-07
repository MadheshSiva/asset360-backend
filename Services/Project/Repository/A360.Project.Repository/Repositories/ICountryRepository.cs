using A360.Repository.Repositories;
using CountryEntity = A360.Project.Domain.Entities.Country;

namespace A360.Project.Repository.Repositories;

public interface ICountryRepository : IMongoRepository<CountryEntity>
{
}
