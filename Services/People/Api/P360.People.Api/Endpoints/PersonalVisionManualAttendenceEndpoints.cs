using P360.People.Api.Contracts;
using P360.People.Api.Validation;
using P360.People.Repository.Repositories;
using P360.Repository.Repositories;

namespace P360.People.Api.Endpoints;

public static class PersonalVisionManualAttendanceEndpoints
{
    public static RouteGroupBuilder MapPersonalVisionManualAttendanceEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/personalvisionmanualattendance")
            .WithTags("Personal Vision Manual Attendance");

        group.MapGet("", GetManualAttendancesAsync)
            .WithName("GetManualAttendances");

        group.MapGet("/{id}", GetManualAttendanceByIdAsync)
            .WithName("GetManualAttendanceById");

        group.MapPost("", CreateManualAttendanceAsync)
            .WithName("CreateManualAttendance");

        group.MapPut("/{id}", UpdateManualAttendanceAsync)
            .WithName("UpdateManualAttendance");

        group.MapDelete("/{id}", DeleteManualAttendanceAsync)
            .WithName("DeleteManualAttendance");

        return group;
    }

    private static async Task<IResult> GetManualAttendancesAsync(
        IPersonalVisionManualAttendanceRepository repository,
        CancellationToken cancellationToken)
    {
        var attendances = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            attendances.Select(
                PersonalVisionManualAttendanceResponse.FromEntity));
    }

    private static async Task<IResult> GetManualAttendanceByIdAsync(
        string id,
        IPersonalVisionManualAttendanceRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid attendance id." });
        }

        var attendance = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return attendance is null
            ? Results.NotFound()
            : Results.Ok(
                PersonalVisionManualAttendanceResponse.FromEntity(
                    attendance));
    }

    private static async Task<IResult> CreateManualAttendanceAsync(
        CreatePersonalVisionManualAttendanceRequest request,
        IPersonalVisionManualAttendanceRepository repository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var attendance = await repository.CreateAsync(
            request.ToEntity(),
            cancellationToken);

        return Results.Created(
            $"/api/personalvisionmanualattendance/{attendance.Id}",
            PersonalVisionManualAttendanceResponse.FromEntity(
                attendance));
    }

    private static async Task<IResult> UpdateManualAttendanceAsync(
        string id,
        UpdatePersonalVisionManualAttendanceRequest request,
        IPersonalVisionManualAttendanceRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid attendance id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var attendance = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (attendance is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(attendance);

        var updated = await repository.UpdateAsync(
            id,
            attendance,
            cancellationToken);

        return updated
            ? Results.Ok(
                PersonalVisionManualAttendanceResponse.FromEntity(
                    attendance))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteManualAttendanceAsync(
        string id,
        IPersonalVisionManualAttendanceRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid attendance id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}