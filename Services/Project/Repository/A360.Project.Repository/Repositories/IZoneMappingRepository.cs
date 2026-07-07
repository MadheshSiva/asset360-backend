using A360.Repository.Repositories;
using ZoneMappingEntity = A360.Project.Domain.Entities.ZoneMapping;

namespace A360.Project.Repository.Repositories;

public interface IZoneMappingRepository : IMongoRepository<ZoneMappingEntity>
{
}
