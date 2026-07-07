using P360.People.Api.Contracts;
using P360.People.Api.Validation;
using P360.People.Repository.Repositories;
using P360.Repository.Repositories;

namespace P360.People.Api.Endpoints;

public static class PersonalVisionAccessEndpoints
{
    public static RouteGroupBuilder MapPersonalVisionAccessEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/personalvisionaccess")
            .WithTags("PersonalVisionAccess");

        group.MapGet("", GetPersonalVisionAccessesAsync)
            .WithName("GetPersonalVisionAccesses");

        group.MapGet("/{id}", GetPersonalVisionAccessByIdAsync)
            .WithName("GetPersonalVisionAccessById");

        group.MapPost("", CreatePersonalVisionAccessAsync)
            .WithName("CreatePersonalVisionAccess");

        group.MapPut("/{id}", UpdatePersonalVisionAccessAsync)
            .WithName("UpdatePersonalVisionAccess");

        group.MapDelete("/{id}", DeletePersonalVisionAccessAsync)
            .WithName("DeletePersonalVisionAccess");

        return group;
    }

    private static async Task<IResult> GetPersonalVisionAccessesAsync(
        IPersonalVisionAccessRepository repository,
        CancellationToken cancellationToken)
    {
        var accesses = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            accesses.Select(PersonalVisionAccessResponse.FromEntity));
    }

    private static async Task<IResult> GetPersonalVisionAccessByIdAsync(
        string id,
        IPersonalVisionAccessRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid PersonalVisionAccess id." });
        }

        var access = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return access is null
            ? Results.NotFound()
            : Results.Ok(
                PersonalVisionAccessResponse.FromEntity(access));
    }

    private static async Task<IResult> CreatePersonalVisionAccessAsync(
        CreatePersonalVisionAccessRequest request,
        IPersonalVisionAccessRepository repository,
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
            $"/api/personalvisionaccess/{access.Id}",
            PersonalVisionAccessResponse.FromEntity(access));
    }

    private static async Task<IResult> UpdatePersonalVisionAccessAsync(
        string id,
        UpdatePersonalVisionAccessRequest request,
        IPersonalVisionAccessRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid PersonalVisionAccess id." });
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
            ? Results.Ok(
                PersonalVisionAccessResponse.FromEntity(access))
            : Results.NotFound();
    }

    private static async Task<IResult> DeletePersonalVisionAccessAsync(
        string id,
        IPersonalVisionAccessRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid PersonalVisionAccess id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}