using A360.Repository.Repositories;
using ModuleAccessMasterEntity = A360.MasterManagement.Domain.Entities.ModuleAccessMaster;

namespace A360.MasterManagement.Repository.Repositories;

public interface IModuleAccessMasterRepository : IMongoRepository<ModuleAccessMasterEntity>
{
}
