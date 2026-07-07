using MongoDB.Driver;
using P360.Repository.Repositories;
using ProjectEntity = P360.Project.Domain.Entities.Project;

namespace P360.Project.Repository.Repositories;

public sealed class ProjectRepository : MongoRepository<ProjectEntity>, IProjectRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "projects";

    public ProjectRepository(IMongoDatabase database)
        : base(database.GetCollection<ProjectEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<ProjectEntity>(
                Builders<ProjectEntity>.IndexKeys.Ascending(project => project.ProjectName),
                new CreateIndexOptions { Name = "ix_projects_project_name" }),
            new CreateIndexModel<ProjectEntity>(
                Builders<ProjectEntity>.IndexKeys
                    .Ascending(project => project.ClientId)
                    .Ascending(project => project.Status),
                new CreateIndexOptions { Name = "ix_projects_client_status" }),
            new CreateIndexModel<ProjectEntity>(
                Builders<ProjectEntity>.IndexKeys
                    .Ascending(project => project.WeekStart)
                    .Ascending(project => project.WeekEnd),
                new CreateIndexOptions { Name = "ix_projects_week_range" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
