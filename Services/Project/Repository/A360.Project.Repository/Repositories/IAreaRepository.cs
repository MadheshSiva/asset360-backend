using A360.Repository.Repositories;
using AreaEntity = A360.Project.Domain.Entities.Area;

namespace A360.Project.Repository.Repositories;

public interface IAreaRepository : IMongoRepository<AreaEntity>
{
}
