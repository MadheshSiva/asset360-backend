using A360.Repository.Repositories;
using A360.VisitorManagement.Api.Contracts;
using A360.VisitorManagement.Api.Validation;
using A360.VisitorManagement.Repository.Repositories;

namespace A360.VisitorManagement.Api.Endpoints;

public static class VisitorEntryExitEndpoints
{
    public static RouteGroupBuilder MapVisitorEntryExitEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/visitorentryexits")
            .WithTags("VisitorEntryExits");

        group.MapGet("", GetAllAsync)
            .WithName("GetVisitorEntryExits");

        group.MapGet("/{id}", GetByIdAsync)
            .WithName("GetVisitorEntryExitById");

        group.MapPost("", CreateAsync)
            .WithName("CreateVisitorEntryExit");

        group.MapPut("/{id}", UpdateAsync)
            .WithName("UpdateVisitorEntryExit");

        group.MapDelete("/{id}", DeleteAsync)
            .WithName("DeleteVisitorEntryExit");

        return group;
    }

    private static async Task<IResult> GetAllAsync(
        IVisitorEntryExitRepository repository,
        CancellationToken cancellationToken)
    {
        var entryExits = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            entryExits.Select(VisitorEntryExitResponse.FromEntity));
    }

    private static async Task<IResult> GetByIdAsync(
        string id,
        IVisitorEntryExitRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid visitor entry/exit id." });
        }

        var entryExit = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return entryExit is null
            ? Results.NotFound()
            : Results.Ok(VisitorEntryExitResponse.FromEntity(entryExit));
    }

    private static async Task<IResult> CreateAsync(
        CreateVisitorEntryExitRequest request,
        IVisitorEntryExitRepository repository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var created = await repository.CreateAsync(
            request.ToEntity(),
            cancellationToken);

        return Results.Created(
            $"/api/visitorentryexits/{created.Id}",
            VisitorEntryExitResponse.FromEntity(created));
    }

    private static async Task<IResult> UpdateAsync(
        string id,
        UpdateVisitorEntryExitRequest request,
        IVisitorEntryExitRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid visitor entry/exit id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var entryExit = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (entryExit is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(entryExit);

        var updated = await repository.UpdateAsync(
            id,
            entryExit,
            cancellationToken);

        return updated
            ? Results.Ok(VisitorEntryExitResponse.FromEntity(entryExit))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteAsync(
        string id,
        IVisitorEntryExitRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid visitor entry/exit id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}
