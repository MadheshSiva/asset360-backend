using A360.Repository.Repositories;
using ResolutionStatusEntity = A360.MasterManagement.Domain.Entities.ResolutionStatus;

namespace A360.MasterManagement.Repository.Repositories;

public interface IResolutionStatusRepository : IMongoRepository<ResolutionStatusEntity>
{
}
