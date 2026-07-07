using P360.Repository.Repositories;
using ZoneMappingEntity = P360.Project.Domain.Entities.ZoneMapping;

namespace P360.Project.Repository.Repositories;

public interface IZoneMappingRepository : IMongoRepository<ZoneMappingEntity>
{
}
