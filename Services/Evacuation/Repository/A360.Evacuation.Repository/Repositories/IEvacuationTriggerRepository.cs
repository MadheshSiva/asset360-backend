
using EvacuationTriggerEntity = A360.Evacuation.Domain.Entities.EvacuationTrigger;
using A360.Repository.Repositories;

namespace A360.Evacuation.Repository.Repositories;

public interface IEvacuationTriggerRepository : IMongoRepository<EvacuationTriggerEntity>
{
}
