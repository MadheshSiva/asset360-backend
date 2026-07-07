using OTSchedulingEntity =
    P360.OTManagement.Domain.Entities.OTScheduling;

using P360.Repository.Repositories;

namespace P360.OTManagement.Repository.Repositories;

public interface IOTSchedulingRepository
    : IMongoRepository<OTSchedulingEntity>
{
}