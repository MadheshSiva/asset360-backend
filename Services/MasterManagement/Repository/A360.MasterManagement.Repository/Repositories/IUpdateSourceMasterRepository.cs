using A360.Repository.Repositories;
using UpdateSourceMasterEntity = A360.MasterManagement.Domain.Entities.UpdateSourceMaster;

namespace A360.MasterManagement.Repository.Repositories;

public interface IUpdateSourceMasterRepository : IMongoRepository<UpdateSourceMasterEntity>
{
}
