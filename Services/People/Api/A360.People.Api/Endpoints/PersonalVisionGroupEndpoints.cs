using A360.People.Api.Contracts;
using A360.People.Api.Validation;
using A360.People.Repository.Repositories;
using A360.Repository.Repositories;

namespace A360.People.Api.Endpoints;

public static class PersonalVisionGroupEndpoints
{
    public static RouteGroupBuilder MapPersonalVisionGroupEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/personalvisiongroups")
            .WithTags("PersonalVisionGroups");

        group.MapGet("", GetGroupsAsync)
            .WithName("GetPersonalVisionGroups");

        group.MapGet("/{id}", GetGroupByIdAsync)
            .WithName("GetPersonalVisionGroupById");

        group.MapPost("", CreateGroupAsync)
            .WithName("CreatePersonalVisionGroup");

        group.MapPut("/{id}", UpdateGroupAsync)
            .WithName("UpdatePersonalVisionGroup");

        group.MapDelete("/{id}", DeleteGroupAsync)
            .WithName("DeletePersonalVisionGroup");

        return group;
    }

    private static async Task<IResult> GetGroupsAsync(
        IPersonalVisionGroupRepository repository,
        CancellationToken cancellationToken)
    {
        var groups = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            groups.Select(
                PersonalVisionGroupResponse.FromEntity));
    }

    private static async Task<IResult> GetGroupByIdAsync(
        string id,
        IPersonalVisionGroupRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid group id." });
        }

        var group = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return group is null
            ? Results.NotFound()
            : Results.Ok(
                PersonalVisionGroupResponse.FromEntity(group));
    }

    private static async Task<IResult> CreateGroupAsync(
        CreatePersonalVisionGroupRequest request,
        IPersonalVisionGroupRepository repository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(
                validationErrors);
        }

        var group = await repository.CreateAsync(
            request.ToEntity(),
            cancellationToken);

        return Results.Created(
            $"/api/personalvisiongroups/{group.Id}",
            PersonalVisionGroupResponse.FromEntity(group));
    }

    private static async Task<IResult> UpdateGroupAsync(
        string id,
        UpdatePersonalVisionGroupRequest request,
        IPersonalVisionGroupRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid group id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(
                validationErrors);
        }

        var group = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (group is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(group);

        var updated = await repository.UpdateAsync(
            id,
            group,
            cancellationToken);

        return updated
            ? Results.Ok(
                PersonalVisionGroupResponse.FromEntity(group))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteGroupAsync(
        string id,
        IPersonalVisionGroupRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid group id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}