using A360.Repository.Repositories;
using ProjectEntity = A360.Project.Domain.Entities.Project;

namespace A360.Project.Repository.Repositories;

public interface IProjectRepository : IMongoRepository<ProjectEntity>
{
}
