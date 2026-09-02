using A360.Repository.Repositories;
using StatusMasterEntity = A360.MasterManagement.Domain.Entities.StatusMaster;

namespace A360.MasterManagement.Repository.Repositories;

public interface IStatusMasterRepository : IMongoRepository<StatusMasterEntity>
{
}
