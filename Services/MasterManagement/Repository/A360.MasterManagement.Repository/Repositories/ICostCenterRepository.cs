using A360.Repository.Repositories;
using CostCenterEntity = A360.MasterManagement.Domain.Entities.CostCenter;

namespace A360.MasterManagement.Repository.Repositories;

public interface ICostCenterRepository : IMongoRepository<CostCenterEntity>
{
}
