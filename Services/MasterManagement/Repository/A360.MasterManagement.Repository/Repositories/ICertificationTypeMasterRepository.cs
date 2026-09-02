using A360.Repository.Repositories;
using CertificationTypeMasterEntity = A360.MasterManagement.Domain.Entities.CertificationTypeMaster;

namespace A360.MasterManagement.Repository.Repositories;

public interface ICertificationTypeMasterRepository : IMongoRepository<CertificationTypeMasterEntity>
{
}
