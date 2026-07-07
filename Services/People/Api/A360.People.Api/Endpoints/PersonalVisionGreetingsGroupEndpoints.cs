using A360.People.Api.Contracts;
using A360.People.Api.Validation;
using A360.People.Repository.Repositories;
using A360.Repository.Repositories;

namespace A360.People.Api.Endpoints;

public static class PersonalVisionGreetingsGroupsEndpoints
{
public static RouteGroupBuilder MapPersonalVisionGreetingsGroupsEndpoints(
this IEndpointRouteBuilder routes)
{
var group = routes.MapGroup("/api/personalvisiongreetingsgroups")
.WithTags("Personal Vision Greetings Groups");


    group.MapGet("", GetGreetingsGroupsAsync)
        .WithName("GetGreetingsGroups");

    group.MapGet("/{id}", GetGreetingsGroupByIdAsync)
        .WithName("GetGreetingsGroupById");

    group.MapPost("", CreateGreetingsGroupAsync)
        .WithName("CreateGreetingsGroup");

    group.MapPut("/{id}", UpdateGreetingsGroupAsync)
        .WithName("UpdateGreetingsGroup");

    group.MapDelete("/{id}", DeleteGreetingsGroupAsync)
        .WithName("DeleteGreetingsGroup");

    return group;
}

private static async Task<IResult> GetGreetingsGroupsAsync(
    IPersonalVisionGreetingsGroupsRepository repository,
    CancellationToken cancellationToken)
{
    var greetingsGroups = await repository.GetAllAsync(
        cancellationToken);

    return Results.Ok(
        greetingsGroups.Select(
            PersonalVisionGreetingsGroupsResponse.FromEntity));
}

private static async Task<IResult> GetGreetingsGroupByIdAsync(
    string id,
    IPersonalVisionGreetingsGroupsRepository repository,
    CancellationToken cancellationToken)
{
    if (!MongoObjectId.IsValid(id))
    {
        return Results.BadRequest(
            new { message = "Invalid greetings group id." });
    }

    var greetingsGroup = await repository.GetByIdAsync(
        id,
        cancellationToken);

    return greetingsGroup is null
        ? Results.NotFound()
        : Results.Ok(
            PersonalVisionGreetingsGroupsResponse.FromEntity(
                greetingsGroup));
}

private static async Task<IResult> CreateGreetingsGroupAsync(
    CreatePersonalVisionGreetingsGroupsRequest request,
    IPersonalVisionGreetingsGroupsRepository repository,
    CancellationToken cancellationToken)
{
    var validationErrors = request.Validate();

    if (validationErrors.Count > 0)
    {
        return Results.ValidationProblem(
            validationErrors);
    }

    var greetingsGroup = await repository.CreateAsync(
        request.ToEntity(),
        cancellationToken);

    return Results.Created(
        $"/api/personalvisiongreetingsgroups/{greetingsGroup.Id}",
        PersonalVisionGreetingsGroupsResponse.FromEntity(
            greetingsGroup));
}

private static async Task<IResult> UpdateGreetingsGroupAsync(
    string id,
    UpdatePersonalVisionGreetingsGroupsRequest request,
    IPersonalVisionGreetingsGroupsRepository repository,
    CancellationToken cancellationToken)
{
    if (!MongoObjectId.IsValid(id))
    {
        return Results.BadRequest(
            new { message = "Invalid greetings group id." });
    }

    var validationErrors = request.Validate();

    if (validationErrors.Count > 0)
    {
        return Results.ValidationProblem(
            validationErrors);
    }

    var greetingsGroup = await repository.GetByIdAsync(
        id,
        cancellationToken);

    if (greetingsGroup is null)
    {
        return Results.NotFound();
    }

    request.ApplyTo(greetingsGroup);

    var updated = await repository.UpdateAsync(
        id,
        greetingsGroup,
        cancellationToken);

    return updated
        ? Results.Ok(
            PersonalVisionGreetingsGroupsResponse.FromEntity(
                greetingsGroup))
        : Results.NotFound();
}

private static async Task<IResult> DeleteGreetingsGroupAsync(
    string id,
    IPersonalVisionGreetingsGroupsRepository repository,
    CancellationToken cancellationToken)
{
    if (!MongoObjectId.IsValid(id))
    {
        return Results.BadRequest(
            new { message = "Invalid greetings group id." });
    }

    var deleted = await repository.DeleteAsync(
        id,
        cancellationToken);

    return deleted
        ? Results.NoContent()
        : Results.NotFound();
}


}
