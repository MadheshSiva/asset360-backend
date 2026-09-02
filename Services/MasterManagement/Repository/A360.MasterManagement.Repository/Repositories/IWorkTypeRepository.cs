using A360.Repository.Repositories;
using WorkTypeEntity = A360.MasterManagement.Domain.Entities.WorkType;

namespace A360.MasterManagement.Repository.Repositories;

public interface IWorkTypeRepository : IMongoRepository<WorkTypeEntity>
{
}
