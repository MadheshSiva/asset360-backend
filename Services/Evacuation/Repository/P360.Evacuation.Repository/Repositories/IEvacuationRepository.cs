
using EvacuationEntity = P360.Evacuation.Domain.Entities.Evacuation;
using P360.Repository.Repositories;

namespace P360.Evacuation.Repository.Repositories;

public interface IEvacuationRepository : IMongoRepository<EvacuationEntity>
{
}
