using A360.People.Api.Contracts;
using A360.People.Api.Validation;
using A360.People.Repository.Repositories;
using A360.Repository.Repositories;

namespace A360.People.Api.Endpoints;

public static class EmployeeEndpoints
{
    public static RouteGroupBuilder MapEmployeeEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/employees")
            .WithTags("Employees");

        group.MapGet("", GetEmployeesAsync)
            .WithName("GetEmployees");

        group.MapGet("/{id}", GetEmployeeByIdAsync)
            .WithName("GetEmployeeById");

        group.MapPost("", CreateEmployeeAsync)
            .WithName("CreateEmployee");

        group.MapPut("/{id}", UpdateEmployeeAsync)
            .WithName("UpdateEmployee");

        group.MapDelete("/{id}", DeleteEmployeeAsync)
            .WithName("DeleteEmployee");

        return group;
    }

    private static async Task<IResult> GetEmployeesAsync(
        IEmployeeRepository repository,
        CancellationToken cancellationToken)
    {
        var employees = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            employees.Select(EmployeeResponse.FromEntity));
    }

    private static async Task<IResult> GetEmployeeByIdAsync(
        string id,
        IEmployeeRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid employee id." });
        }

        var employee = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return employee is null
            ? Results.NotFound()
            : Results.Ok(EmployeeResponse.FromEntity(employee));
    }

    private static async Task<IResult> CreateEmployeeAsync(
        CreateEmployeeRequest request,
        IEmployeeRepository repository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var employee = await repository.CreateAsync(
            request.ToEntity(),
            cancellationToken);

        return Results.Created(
            $"/api/employees/{employee.Id}",
            EmployeeResponse.FromEntity(employee));
    }

    private static async Task<IResult> UpdateEmployeeAsync(
        string id,
        UpdateEmployeeRequest request,
        IEmployeeRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid employee id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var employee = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (employee is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(employee);

        var updated = await repository.UpdateAsync(
            id,
            employee,
            cancellationToken);

        return updated
            ? Results.Ok(EmployeeResponse.FromEntity(employee))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteEmployeeAsync(
        string id,
        IEmployeeRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid employee id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}