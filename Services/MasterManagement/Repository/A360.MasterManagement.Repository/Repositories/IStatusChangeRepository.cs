using A360.Repository.Repositories;
using StatusChangeEntity = A360.MasterManagement.Domain.Entities.StatusChange;

namespace A360.MasterManagement.Repository.Repositories;

public interface IStatusChangeRepository : IMongoRepository<StatusChangeEntity>
{
}
