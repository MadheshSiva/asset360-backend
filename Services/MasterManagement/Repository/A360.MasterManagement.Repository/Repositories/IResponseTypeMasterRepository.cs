using A360.Repository.Repositories;
using ResponseTypeMasterEntity = A360.MasterManagement.Domain.Entities.ResponseTypeMaster;

namespace A360.MasterManagement.Repository.Repositories;

public interface IResponseTypeMasterRepository : IMongoRepository<ResponseTypeMasterEntity>
{
}
