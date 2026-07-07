using BuildingEntity = P360.Project.Domain.Entities.Building;
using P360.Repository.Repositories;

namespace P360.Project.Repository.Repositories;

public interface IBuildingRepository : IMongoRepository<BuildingEntity>
{
}
