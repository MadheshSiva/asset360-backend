using BuildingEntity = A360.Project.Domain.Entities.Building;
using A360.Repository.Repositories;

namespace A360.Project.Repository.Repositories;

public interface IBuildingRepository : IMongoRepository<BuildingEntity>
{
}
