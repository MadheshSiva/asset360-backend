using OTSchedulingEntity =
    A360.OTManagement.Domain.Entities.OTScheduling;

using A360.Repository.Repositories;

namespace A360.OTManagement.Repository.Repositories;

public interface IOTSchedulingRepository
    : IMongoRepository<OTSchedulingEntity>
{
}