using P360.People.Api.Contracts;
using P360.People.Api.Validation;
using P360.People.Repository.Repositories;
using P360.Repository.Repositories;

namespace P360.People.Api.Endpoints;

public static class AccessEndpoints
{
    public static RouteGroupBuilder MapAccessEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/access")
            .WithTags("Access");

        group.MapGet("", GetAccessesAsync)
            .WithName("GetAccesses");

        group.MapGet("/{id}", GetAccessByIdAsync)
            .WithName("GetAccessById");

        group.MapPost("", CreateAccessAsync)
            .WithName("CreateAccess");

        group.MapPut("/{id}", UpdateAccessAsync)
            .WithName("UpdateAccess");

        group.MapDelete("/{id}", DeleteAccessAsync)
            .WithName("DeleteAccess");

        return group;
    }

    private static async Task<IResult> GetAccessesAsync(
        IAccessRepository repository,
        CancellationToken cancellationToken)
    {
        var accesses = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            accesses.Select(AccessResponse.FromEntity));
    }

    private static async Task<IResult> GetAccessByIdAsync(
        string id,
        IAccessRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid access id." });
        }

        var access = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return access is null
            ? Results.NotFound()
            : Results.Ok(AccessResponse.FromEntity(access));
    }

    private static async Task<IResult> CreateAccessAsync(
        CreateAccessRequest request,
        IAccessRepository repository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var access = await repository.CreateAsync(
            request.ToEntity(),
            cancellationToken);

        return Results.Created(
            $"/api/access/{access.Id}",
            AccessResponse.FromEntity(access));
    }

    private static async Task<IResult> UpdateAccessAsync(
        string id,
        UpdateAccessRequest request,
        IAccessRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid access id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var access = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (access is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(access);

        var updated = await repository.UpdateAsync(
            id,
            access,
            cancellationToken);

        return updated
            ? Results.Ok(AccessResponse.FromEntity(access))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteAccessAsync(
        string id,
        IAccessRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid access id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}