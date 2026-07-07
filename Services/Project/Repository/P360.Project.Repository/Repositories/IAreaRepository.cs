using P360.Repository.Repositories;
using AreaEntity = P360.Project.Domain.Entities.Area;

namespace P360.Project.Repository.Repositories;

public interface IAreaRepository : IMongoRepository<AreaEntity>
{
}
