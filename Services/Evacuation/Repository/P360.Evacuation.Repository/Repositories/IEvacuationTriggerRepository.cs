
using EvacuationTriggerEntity = P360.Evacuation.Domain.Entities.EvacuationTrigger;
using P360.Repository.Repositories;

namespace P360.Evacuation.Repository.Repositories;

public interface IEvacuationTriggerRepository : IMongoRepository<EvacuationTriggerEntity>
{
}
