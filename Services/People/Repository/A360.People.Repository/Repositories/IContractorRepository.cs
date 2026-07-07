using ContractorEntity = A360.People.Domain.Entities.Contractor;
using A360.Repository.Repositories;

namespace A360.People.Repository.Repositories;

public interface IContractorRepository
    : IMongoRepository<ContractorEntity>
{
}