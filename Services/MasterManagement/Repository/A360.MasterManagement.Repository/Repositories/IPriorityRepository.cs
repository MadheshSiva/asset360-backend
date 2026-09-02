using A360.Repository.Repositories;
using PriorityEntity = A360.MasterManagement.Domain.Entities.Priority;

namespace A360.MasterManagement.Repository.Repositories;

public interface IPriorityRepository : IMongoRepository<PriorityEntity>
{
}
