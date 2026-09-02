using A360.Repository.Repositories;
using MasterMaintenanceEntity = A360.MasterManagement.Domain.Entities.MasterMaintenance;

namespace A360.MasterManagement.Repository.Repositories;

public interface IMasterMaintenanceRepository : IMongoRepository<MasterMaintenanceEntity>
{
}
