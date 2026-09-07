using A360.Asset.Repository.Repositories;
using A360.Domain.Entities;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Activity;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class DepartmentEndpoints
{
    private const string SequenceName = "department";
    private const string DepartmentCodePrefix = "DEPT";

    public static RouteGroupBuilder MapDepartmentEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/departments").WithTags("Departments");

        group.MapGet("", GetDepartmentsAsync).WithName("GetDepartments");
        group.MapGet("/{id}", GetDepartmentByIdAsync).WithName("GetDepartmentById");
        group.MapPost("", CreateDepartmentAsync).WithName("CreateDepartment");
        group.MapPut("/{id}", UpdateDepartmentAsync).WithName("UpdateDepartment");
        group.MapDelete("/{id}", DeleteDepartmentAsync).WithName("DeleteDepartment");

        return group;
    }

    private static async Task<IResult> GetDepartmentsAsync(
        IDepartmentRepository repository,
        CancellationToken cancellationToken)
    {
        var departments = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(departments.Select(DepartmentResponse.FromEntity));
    }

    private static async Task<IResult> GetDepartmentByIdAsync(
        string id,
        IDepartmentRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid department id." });
        }

        var department = await repository.GetByIdAsync(id, cancellationToken);
        if (department is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("Department", department.DepartmentCode, department.DepartmentName, EventAction.Viewed, cancellationToken);

        return Results.Ok(DepartmentResponse.FromEntity(department));
    }

    private static async Task<IResult> CreateDepartmentAsync(
        CreateDepartmentRequest request,
        IDepartmentRepository repository,
        IAssetRepository assetRepository,
        IBusinessUnitRepository businessUnitRepository,
        ISequenceGenerator sequenceGenerator,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var asset = await assetRepository.GetByAssetIdAsync(request.AssetId!, cancellationToken);
        if (asset is null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["AssetId"] = ["No asset exists with this AssetId"]
            });
        }

        var businessUnit = await businessUnitRepository.GetByBusinessUnitCodeAsync(request.BusinessUnit!, cancellationToken);
        if (businessUnit is null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["BusinessUnit"] = ["No business unit exists with this BusinessUnit code"]
            });
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var departmentCode = $"{DepartmentCodePrefix}{nextSequence:D6}";

        var department = await repository.CreateAsync(
            request.ToEntity(departmentCode, asset.AssetName),
            cancellationToken);

        await eventLogger.LogAsync("Department", department.DepartmentCode, department.DepartmentName, EventAction.Created, cancellationToken);

        return Results.Created($"/api/departments/{department.Id}", DepartmentResponse.FromEntity(department));
    }

    private static async Task<IResult> UpdateDepartmentAsync(
        string id,
        UpdateDepartmentRequest request,
        IDepartmentRepository repository,
        IAssetRepository assetRepository,
        IBusinessUnitRepository businessUnitRepository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid department id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var department = await repository.GetByIdAsync(id, cancellationToken);
        if (department is null)
        {
            return Results.NotFound();
        }

        var asset = await assetRepository.GetByAssetIdAsync(request.AssetId!, cancellationToken);
        if (asset is null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["AssetId"] = ["No asset exists with this AssetId"]
            });
        }

        var businessUnit = await businessUnitRepository.GetByBusinessUnitCodeAsync(request.BusinessUnit!, cancellationToken);
        if (businessUnit is null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["BusinessUnit"] = ["No business unit exists with this BusinessUnit code"]
            });
        }

        request.ApplyTo(department, asset.AssetName);

        var updated = await repository.UpdateAsync(id, department, cancellationToken);
        if (updated)
        {
            await eventLogger.LogAsync("Department", department.DepartmentCode, department.DepartmentName, EventAction.Updated, cancellationToken);
        }

        return updated ? Results.Ok(DepartmentResponse.FromEntity(department)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteDepartmentAsync(
        string id,
        IDepartmentRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid department id." });
        }

        var department = await repository.GetByIdAsync(id, cancellationToken);
        if (department is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (deleted)
        {
            await eventLogger.LogAsync("Department", department.DepartmentCode, department.DepartmentName, EventAction.Deleted, cancellationToken);
        }

        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
