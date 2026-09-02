using A360.Repository.Repositories;
using SeverityMasterEntity = A360.MasterManagement.Domain.Entities.SeverityMaster;

namespace A360.MasterManagement.Repository.Repositories;

public interface ISeverityMasterRepository : IMongoRepository<SeverityMasterEntity>
{
}
