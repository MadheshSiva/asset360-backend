using A360.Repository.Repositories;
using ConditionMasterEntity = A360.MasterManagement.Domain.Entities.ConditionMaster;

namespace A360.MasterManagement.Repository.Repositories;

public interface IConditionMasterRepository : IMongoRepository<ConditionMasterEntity>
{
}
