using A360.People.Api.Contracts;
using A360.People.Api.Validation;
using A360.People.Repository.Repositories;
using A360.Repository.Repositories;

namespace A360.People.Api.Endpoints;

public static class PersonalWorkScheduleEndpoints
{
    public static RouteGroupBuilder MapPersonalWorkScheduleEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/personalworkschedules")
            .WithTags("PersonalWorkSchedules");

        group.MapGet("", GetPersonalWorkSchedulesAsync)
            .WithName("GetPersonalWorkSchedules");

        group.MapGet("/{id}", GetPersonalWorkScheduleByIdAsync)
            .WithName("GetPersonalWorkScheduleById");

        group.MapPost("", CreatePersonalWorkScheduleAsync)
            .WithName("CreatePersonalWorkSchedule");

        group.MapPut("/{id}", UpdatePersonalWorkScheduleAsync)
            .WithName("UpdatePersonalWorkSchedule");

        group.MapDelete("/{id}", DeletePersonalWorkScheduleAsync)
            .WithName("DeletePersonalWorkSchedule");

        return group;
    }

    private static async Task<IResult> GetPersonalWorkSchedulesAsync(
        IPersonalWorkScheduleRepository repository,
        CancellationToken cancellationToken)
    {
        var schedules = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            schedules.Select(
                PersonalWorkScheduleResponse.FromEntity));
    }

    private static async Task<IResult> GetPersonalWorkScheduleByIdAsync(
        string id,
        IPersonalWorkScheduleRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid work schedule id." });
        }

        var schedule = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return schedule is null
            ? Results.NotFound()
            : Results.Ok(
                PersonalWorkScheduleResponse.FromEntity(
                    schedule));
    }

    private static async Task<IResult> CreatePersonalWorkScheduleAsync(
        CreatePersonalWorkScheduleRequest request,
        IPersonalWorkScheduleRepository repository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(
                validationErrors);
        }

        var schedule = await repository.CreateAsync(
            request.ToEntity(),
            cancellationToken);

        return Results.Created(
            $"/api/personalworkschedules/{schedule.Id}",
            PersonalWorkScheduleResponse.FromEntity(
                schedule));
    }

    private static async Task<IResult> UpdatePersonalWorkScheduleAsync(
        string id,
        UpdatePersonalWorkScheduleRequest request,
        IPersonalWorkScheduleRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid work schedule id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(
                validationErrors);
        }

        var schedule = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (schedule is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(schedule);

        var updated = await repository.UpdateAsync(
            id,
            schedule,
            cancellationToken);

        return updated
            ? Results.Ok(
                PersonalWorkScheduleResponse.FromEntity(
                    schedule))
            : Results.NotFound();
    }

    private static async Task<IResult> DeletePersonalWorkScheduleAsync(
        string id,
        IPersonalWorkScheduleRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid work schedule id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}