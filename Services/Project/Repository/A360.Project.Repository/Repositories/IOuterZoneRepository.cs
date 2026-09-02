using A360.Repository.Repositories;
using OuterZoneEntity = A360.Project.Domain.Entities.OuterZone;

namespace A360.Project.Repository.Repositories;

public interface IOuterZoneRepository : IMongoRepository<OuterZoneEntity>
{
}
