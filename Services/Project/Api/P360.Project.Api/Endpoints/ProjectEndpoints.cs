using P360.Project.Api.Contracts;
using P360.Project.Api.Validation;
using P360.Project.Repository.Repositories;
using P360.Repository.Repositories;

namespace P360.Project.Api.Endpoints;

public static class ProjectEndpoints
{
    public static RouteGroupBuilder MapProjectEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/projects").WithTags("Projects");

        group.MapGet("", GetProjectsAsync).WithName("GetProjects");
        group.MapGet("/{id}", GetProjectByIdAsync).WithName("GetProjectById");
        group.MapPost("", CreateProjectAsync).WithName("CreateProject");
        group.MapPut("/{id}", UpdateProjectAsync).WithName("UpdateProject");
        group.MapDelete("/{id}", DeleteProjectAsync).WithName("DeleteProject");

        return group;
    }

    private static async Task<IResult> GetProjectsAsync(
        IProjectRepository repository,
        CancellationToken cancellationToken)
    {
        var projects = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(projects.Select(ProjectResponse.FromEntity));
    }

    private static async Task<IResult> GetProjectByIdAsync(
        string id,
        IProjectRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid project id." });
        }

        var project = await repository.GetByIdAsync(id, cancellationToken);
        return project is null
            ? Results.NotFound()
            : Results.Ok(ProjectResponse.FromEntity(project));
    }

    private static async Task<IResult> CreateProjectAsync(
        CreateProjectRequest request,
        IProjectRepository repository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var project = await repository.CreateAsync(request.ToEntity(), cancellationToken);

        return Results.Created(
            $"/api/projects/{project.Id}",
            ProjectResponse.FromEntity(project));
    }

    private static async Task<IResult> UpdateProjectAsync(
        string id,
        UpdateProjectRequest request,
        IProjectRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid project id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var project = await repository.GetByIdAsync(id, cancellationToken);
        if (project is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(project);

        var updated = await repository.UpdateAsync(id, project, cancellationToken);
        return updated
            ? Results.Ok(ProjectResponse.FromEntity(project))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteProjectAsync(
        string id,
        IProjectRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid project id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
