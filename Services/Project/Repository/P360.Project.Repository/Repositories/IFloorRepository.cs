using FloorEntity = P360.Project.Domain.Entities.Floor;
using P360.Repository.Repositories;

namespace P360.Project.Repository.Repositories;

public interface IFloorRepository : IMongoRepository<FloorEntity>
{
}
