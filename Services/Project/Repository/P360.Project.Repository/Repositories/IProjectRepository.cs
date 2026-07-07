using P360.Repository.Repositories;
using ProjectEntity = P360.Project.Domain.Entities.Project;

namespace P360.Project.Repository.Repositories;

public interface IProjectRepository : IMongoRepository<ProjectEntity>
{
}
