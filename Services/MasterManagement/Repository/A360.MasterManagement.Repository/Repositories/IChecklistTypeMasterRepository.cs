using A360.Repository.Repositories;
using ChecklistTypeMasterEntity = A360.MasterManagement.Domain.Entities.ChecklistTypeMaster;

namespace A360.MasterManagement.Repository.Repositories;

public interface IChecklistTypeMasterRepository : IMongoRepository<ChecklistTypeMasterEntity>
{
}
