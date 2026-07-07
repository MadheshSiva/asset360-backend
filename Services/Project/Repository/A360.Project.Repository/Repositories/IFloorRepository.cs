using FloorEntity = A360.Project.Domain.Entities.Floor;
using A360.Repository.Repositories;

namespace A360.Project.Repository.Repositories;

public interface IFloorRepository : IMongoRepository<FloorEntity>
{
}
