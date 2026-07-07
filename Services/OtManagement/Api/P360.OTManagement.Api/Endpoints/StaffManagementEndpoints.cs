using P360.OTManagement.Api.Contracts;
using P360.OTManagement.Api.Validation;
using P360.OTManagement.Repository.Repositories;
using P360.Repository.Repositories;

namespace P360.OTManagement.Api.Endpoints;

public static class StaffManagementEndpoints
{
    public static RouteGroupBuilder MapStaffManagementEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/staff")
            .WithTags("StaffManagement");

        group.MapGet("", GetStaffsAsync)
            .WithName("GetStaffs");

        group.MapGet("/{id}", GetStaffByIdAsync)
            .WithName("GetStaffById");

        group.MapPost("", CreateStaffAsync)
            .WithName("CreateStaff");

        group.MapPut("/{id}", UpdateStaffAsync)
            .WithName("UpdateStaff");

        group.MapDelete("/{id}", DeleteStaffAsync)
            .WithName("DeleteStaff");

        return group;
    }

    private static async Task<IResult> GetStaffsAsync(
        IStaffManagementRepository repository,
        CancellationToken cancellationToken)
    {
        var staffs = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            staffs.Select(
                StaffManagementResponse.FromEntity));
    }

    private static async Task<IResult> GetStaffByIdAsync(
        string id,
        IStaffManagementRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid Staff id." });
        }

        var staff = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return staff is null
            ? Results.NotFound()
            : Results.Ok(
                StaffManagementResponse.FromEntity(
                    staff));
    }

    private static async Task<IResult> CreateStaffAsync(
        CreateStaffManagementRequest request,
        IStaffManagementRepository repository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(
                validationErrors);
        }

        var staff = await repository.CreateAsync(
            request.ToEntity(),
            cancellationToken);

        return Results.Created(
            $"/api/staff/{staff.Id}",
            StaffManagementResponse.FromEntity(
                staff));
    }

    private static async Task<IResult> UpdateStaffAsync(
        string id,
        UpdateStaffManagementRequest request,
        IStaffManagementRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid Staff id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(
                validationErrors);
        }

        var staff = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (staff is null)
        {
            return Results.NotFound();
        }

        request.UpdateEntity(staff);

        var updated = await repository.UpdateAsync(
            id,
            staff,
            cancellationToken);

        return updated
            ? Results.Ok(
                StaffManagementResponse.FromEntity(
                    staff))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteStaffAsync(
        string id,
        IStaffManagementRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid Staff id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}