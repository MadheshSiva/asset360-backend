using A360.Repository.Repositories;
using ApiSyncStatusMasterEntity = A360.MasterManagement.Domain.Entities.ApiSyncStatusMaster;

namespace A360.MasterManagement.Repository.Repositories;

public interface IApiSyncStatusMasterRepository : IMongoRepository<ApiSyncStatusMasterEntity>
{
}
