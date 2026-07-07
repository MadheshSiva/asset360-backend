using P360.People.Api.Contracts;
using P360.People.Api.Validation;
using P360.People.Repository.Repositories;
using P360.Repository.Repositories;

namespace P360.People.Api.Endpoints;

public static class GroupEndpoints
{
    public static RouteGroupBuilder MapGroupEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/groups")
            .WithTags("Groups");

        group.MapGet("", GetGroupsAsync)
            .WithName("GetGroups");

        group.MapGet("/{id}", GetGroupByIdAsync)
            .WithName("GetGroupById");

        group.MapPost("", CreateGroupAsync)
            .WithName("CreateGroup");

        group.MapPut("/{id}", UpdateGroupAsync)
            .WithName("UpdateGroup");

        group.MapDelete("/{id}", DeleteGroupAsync)
            .WithName("DeleteGroup");

        group.MapGet("/type/{groupType}", GetGroupsByTypeAsync)
             .WithName("GetGroupsByType");

        group.MapGet("/{groupId}/members",GetGroupMembersAsync)
             .WithName("GetGroupMembers");

        return group;
    }

    private static async Task<IResult> GetGroupsAsync(
        IGroupRepository repository,
        CancellationToken cancellationToken)
    {
        var groups = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            groups.Select(GroupResponse.FromEntity));
    }

    private static async Task<IResult> GetGroupByIdAsync(
        string id,
        IGroupRepository repository,
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
            : Results.Ok(GroupResponse.FromEntity(group));
    }

    private static async Task<IResult> CreateGroupAsync(
        CreateGroupRequest request,
        IGroupRepository repository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var group = await repository.CreateAsync(
            request.ToEntity(),
            cancellationToken);

        return Results.Created(
            $"/api/groups/{group.Id}",
            GroupResponse.FromEntity(group));
    }

    private static async Task<IResult> UpdateGroupAsync(
        string id,
        UpdateGroupRequest request,
        IGroupRepository repository,
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
            return Results.ValidationProblem(validationErrors);
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
            ? Results.Ok(GroupResponse.FromEntity(group))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteGroupAsync(
        string id,
        IGroupRepository repository,
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

    private static async Task<IResult> GetGroupsByTypeAsync(
    string groupType,
    IGroupRepository repository,
    CancellationToken cancellationToken)
{
    var groups = await repository.GetByGroupTypeAsync(
        groupType,
        cancellationToken);

    return Results.Ok(
        groups.Select(GroupResponse.FromEntity));
}


private static async Task<IResult> GetGroupMembersAsync(
    string groupId,
    IGroupRepository groupRepository,
    CancellationToken cancellationToken)
{
    var group = await groupRepository.GetByIdAsync(
        groupId,
        cancellationToken);

    if (group is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(group.Members);
}
}