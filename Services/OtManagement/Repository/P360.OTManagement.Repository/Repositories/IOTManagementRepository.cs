using OTManagementEntity =
    P360.OTManagement.Domain.Entities.OTManagement;

using P360.Repository.Repositories;

namespace P360.OTManagement.Repository.Repositories;

public interface IOTManagementRepository
    : IMongoRepository<OTManagementEntity>
{
}