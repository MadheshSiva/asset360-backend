using A360.Repository.Repositories;
using UnitMasterEntity = A360.MasterManagement.Domain.Entities.UnitMaster;

namespace A360.MasterManagement.Repository.Repositories;

public interface IUnitMasterRepository : IMongoRepository<UnitMasterEntity>
{
}
