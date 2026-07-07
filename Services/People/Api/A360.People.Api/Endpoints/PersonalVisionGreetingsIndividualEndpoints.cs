using A360.People.Api.Contracts;
using A360.People.Api.Validation;
using A360.People.Repository.Repositories;
using A360.Repository.Repositories;

namespace A360.People.Api.Endpoints;

public static class PersonalVisionGreetingsIndividualEndpoints
{
public static RouteGroupBuilder MapPersonalVisionGreetingsIndividualEndpoints(
this IEndpointRouteBuilder routes)
{
var group = routes.MapGroup("/api/personalvisiongreetingsindividual")
.WithTags("Personal Vision Greetings Individual");


    group.MapGet("", GetGreetingsAsync)
        .WithName("GetGreetings");

    group.MapGet("/{id}", GetGreetingByIdAsync)
        .WithName("GetGreetingById");

    group.MapPost("", CreateGreetingAsync)
        .WithName("CreateGreeting");

    group.MapPut("/{id}", UpdateGreetingAsync)
        .WithName("UpdateGreeting");

    group.MapDelete("/{id}", DeleteGreetingAsync)
        .WithName("DeleteGreeting");

    return group;
}

private static async Task<IResult> GetGreetingsAsync(
    IPersonalVisionGreetingsIndividualRepository repository,
    CancellationToken cancellationToken)
{
    var greetings = await repository.GetAllAsync(
        cancellationToken);

    return Results.Ok(
        greetings.Select(
            PersonalVisionGreetingsIndividualResponse.FromEntity));
}

private static async Task<IResult> GetGreetingByIdAsync(
    string id,
    IPersonalVisionGreetingsIndividualRepository repository,
    CancellationToken cancellationToken)
{
    if (!MongoObjectId.IsValid(id))
    {
        return Results.BadRequest(
            new { message = "Invalid greeting id." });
    }

    var greeting = await repository.GetByIdAsync(
        id,
        cancellationToken);

    return greeting is null
        ? Results.NotFound()
        : Results.Ok(
            PersonalVisionGreetingsIndividualResponse.FromEntity(
                greeting));
}

private static async Task<IResult> CreateGreetingAsync(
    CreatePersonalVisionGreetingsIndividualRequest request,
    IPersonalVisionGreetingsIndividualRepository repository,
    CancellationToken cancellationToken)
{
    var validationErrors = request.Validate();

    if (validationErrors.Count > 0)
    {
        return Results.ValidationProblem(
            validationErrors);
    }

    var greeting = await repository.CreateAsync(
        request.ToEntity(),
        cancellationToken);

    return Results.Created(
        $"/api/personalvisiongreetingsindividual/{greeting.Id}",
        PersonalVisionGreetingsIndividualResponse.FromEntity(
            greeting));
}

private static async Task<IResult> UpdateGreetingAsync(
    string id,
    UpdatePersonalVisionGreetingsIndividualRequest request,
    IPersonalVisionGreetingsIndividualRepository repository,
    CancellationToken cancellationToken)
{
    if (!MongoObjectId.IsValid(id))
    {
        return Results.BadRequest(
            new { message = "Invalid greeting id." });
    }

    var validationErrors = request.Validate();

    if (validationErrors.Count > 0)
    {
        return Results.ValidationProblem(
            validationErrors);
    }

    var greeting = await repository.GetByIdAsync(
        id,
        cancellationToken);

    if (greeting is null)
    {
        return Results.NotFound();
    }

    request.ApplyTo(greeting);

    var updated = await repository.UpdateAsync(
        id,
        greeting,
        cancellationToken);

    return updated
        ? Results.Ok(
            PersonalVisionGreetingsIndividualResponse.FromEntity(
                greeting))
        : Results.NotFound();
}

private static async Task<IResult> DeleteGreetingAsync(
    string id,
    IPersonalVisionGreetingsIndividualRepository repository,
    CancellationToken cancellationToken)
{
    if (!MongoObjectId.IsValid(id))
    {
        return Results.BadRequest(
            new { message = "Invalid greeting id." });
    }

    var deleted = await repository.DeleteAsync(
        id,
        cancellationToken);

    return deleted
        ? Results.NoContent()
        : Results.NotFound();
}


}
