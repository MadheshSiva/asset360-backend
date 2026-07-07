using ContractorEntity = P360.People.Domain.Entities.Contractor;
using P360.Repository.Repositories;

namespace P360.People.Repository.Repositories;

public interface IContractorRepository
    : IMongoRepository<ContractorEntity>
{
}