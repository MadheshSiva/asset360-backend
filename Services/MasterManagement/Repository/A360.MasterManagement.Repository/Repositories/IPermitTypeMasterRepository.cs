using A360.Repository.Repositories;
using PermitTypeMasterEntity = A360.MasterManagement.Domain.Entities.PermitTypeMaster;

namespace A360.MasterManagement.Repository.Repositories;

public interface IPermitTypeMasterRepository : IMongoRepository<PermitTypeMasterEntity>
{
}
