using A360.Domain.Entities;
using A360.Project.Api.Contracts;
using A360.Project.Api.Validation;
using A360.Project.Repository.Repositories;
using A360.Repository.Activity;
using A360.Repository.Repositories;

namespace A360.Project.Api.Endpoints;

public static class AreaEndpoints
{
    public static RouteGroupBuilder MapAreaEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/areas").WithTags("Areas");

        group.MapGet("", GetAreasAsync).WithName("GetAreas");
        group.MapGet("/{id}", GetAreaByIdAsync).WithName("GetAreaById");
        group.MapPost("", CreateAreaAsync).WithName("CreateArea");
        group.MapPut("/{id}", UpdateAreaAsync).WithName("UpdateArea");
        group.MapDelete("/{id}", DeleteAreaAsync).WithName("DeleteArea");

        return group;
    }

    private static async Task<IResult> GetAreasAsync(
        IAreaRepository repository,
        CancellationToken cancellationToken)
    {
        var areas = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(areas.Select(AreaResponse.FromEntity));
    }

    private static async Task<IResult> GetAreaByIdAsync(
        string id,
        IAreaRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid area id." });
        }

        var area = await repository.GetByIdAsync(id, cancellationToken);
        if (area is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("Area", area.Id, area.AreaName, EventAction.Viewed, cancellationToken);

        return Results.Ok(AreaResponse.FromEntity(area));
    }

    private static async Task<IResult> CreateAreaAsync(
        CreateAreaRequest request,
        IAreaRepository repository,
        IProjectRepository projectRepository,
        ICountryRepository countryRepository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var relationshipErrors = await request.ValidateRelationshipsAsync(
            projectRepository,
            countryRepository,
            cancellationToken);
        if (relationshipErrors.Count > 0)
        {
            return Results.ValidationProblem(relationshipErrors);
        }

        var area = await repository.CreateAsync(request.ToEntity(), cancellationToken);

        await eventLogger.LogAsync("Area", area.Id, area.AreaName, EventAction.Created, cancellationToken);

        return Results.Created($"/api/areas/{area.Id}", AreaResponse.FromEntity(area));
    }

    private static async Task<IResult> UpdateAreaAsync(
        string id,
        UpdateAreaRequest request,
        IAreaRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid area id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var area = await repository.GetByIdAsync(id, cancellationToken);
        if (area is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(area);

        var updated = await repository.UpdateAsync(id, area, cancellationToken);
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("Area", area.Id, area.AreaName, EventAction.Updated, cancellationToken);

        return Results.Ok(AreaResponse.FromEntity(area));
    }

    private static async Task<IResult> DeleteAreaAsync(
        string id,
        IAreaRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid area id." });
        }

        var area = await repository.GetByIdAsync(id, cancellationToken);
        if (area is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("Area", area.Id, area.AreaName, EventAction.Deleted, cancellationToken);

        return Results.NoContent();
    }
}
