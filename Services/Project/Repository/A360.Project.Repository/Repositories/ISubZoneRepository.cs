using A360.Repository.Repositories;
using SubZoneEntity = A360.Project.Domain.Entities.SubZone;

namespace A360.Project.Repository.Repositories;

public interface ISubZoneRepository : IMongoRepository<SubZoneEntity>
{
}
