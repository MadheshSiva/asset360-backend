using P360.Repository.Repositories;
using SubZoneEntity = P360.Project.Domain.Entities.SubZone;

namespace P360.Project.Repository.Repositories;

public interface ISubZoneRepository : IMongoRepository<SubZoneEntity>
{
}
