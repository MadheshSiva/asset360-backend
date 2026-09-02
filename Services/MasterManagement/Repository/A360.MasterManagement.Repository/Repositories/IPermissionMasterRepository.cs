using A360.Repository.Repositories;
using PermissionMasterEntity = A360.MasterManagement.Domain.Entities.PermissionMaster;

namespace A360.MasterManagement.Repository.Repositories;

public interface IPermissionMasterRepository : IMongoRepository<PermissionMasterEntity>
{
}
