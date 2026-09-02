using A360.Repository.Repositories;
using ShiftMasterEntity = A360.MasterManagement.Domain.Entities.ShiftMaster;

namespace A360.MasterManagement.Repository.Repositories;

public interface IShiftMasterRepository : IMongoRepository<ShiftMasterEntity>
{
}
