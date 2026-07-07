using P360.Repository.Repositories;
using ZoneEntity = P360.Project.Domain.Entities.Zone;

namespace P360.Project.Repository.Repositories;

public interface IZoneRepository : IMongoRepository<ZoneEntity>
{
}
