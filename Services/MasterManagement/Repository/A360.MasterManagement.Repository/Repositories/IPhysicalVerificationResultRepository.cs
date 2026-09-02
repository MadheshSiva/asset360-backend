using A360.Repository.Repositories;
using PhysicalVerificationResultEntity = A360.MasterManagement.Domain.Entities.PhysicalVerificationResult;

namespace A360.MasterManagement.Repository.Repositories;

public interface IPhysicalVerificationResultRepository : IMongoRepository<PhysicalVerificationResultEntity>
{
}
