using A360.Repository.Repositories;
using ZoneEntity = A360.Project.Domain.Entities.Zone;

namespace A360.Project.Repository.Repositories;

public interface IZoneRepository : IMongoRepository<ZoneEntity>
{
}
