using OTManagementEntity =
    A360.OTManagement.Domain.Entities.OTManagement;

using A360.Repository.Repositories;

namespace A360.OTManagement.Repository.Repositories;

public interface IOTManagementRepository
    : IMongoRepository<OTManagementEntity>
{
}