
using EvacuationEntity = A360.Evacuation.Domain.Entities.Evacuation;
using A360.Repository.Repositories;

namespace A360.Evacuation.Repository.Repositories;

public interface IEvacuationRepository : IMongoRepository<EvacuationEntity>
{
}
