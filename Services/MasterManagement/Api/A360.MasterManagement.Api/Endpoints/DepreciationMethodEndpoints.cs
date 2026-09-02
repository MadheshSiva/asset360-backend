using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class DepreciationMethodEndpoints
{
    private const string SequenceName = "depreciation_method";
    private const string MethodIdPrefix = "DEP";

    public static RouteGroupBuilder MapDepreciationMethodEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/depreciation-methods").WithTags("DepreciationMethods");

        group.MapGet("", GetDepreciationMethodsAsync).WithName("GetDepreciationMethods");
        group.MapGet("/{id}", GetDepreciationMethodByIdAsync).WithName("GetDepreciationMethodById");
        group.MapPost("", CreateDepreciationMethodAsync).WithName("CreateDepreciationMethod");
        group.MapPut("/{id}", UpdateDepreciationMethodAsync).WithName("UpdateDepreciationMethod");
        group.MapDelete("/{id}", DeleteDepreciationMethodAsync).WithName("DeleteDepreciationMethod");

        return group;
    }

    private static async Task<IResult> GetDepreciationMethodsAsync(
        IDepreciationMethodRepository repository,
        CancellationToken cancellationToken)
    {
        var depreciationMethods = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(depreciationMethods.Select(DepreciationMethodResponse.FromEntity));
    }

    private static async Task<IResult> GetDepreciationMethodByIdAsync(
        string id,
        IDepreciationMethodRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid depreciation method id." });
        }

        var depreciationMethod = await repository.GetByIdAsync(id, cancellationToken);
        return depreciationMethod is null ? Results.NotFound() : Results.Ok(DepreciationMethodResponse.FromEntity(depreciationMethod));
    }

    private static async Task<IResult> CreateDepreciationMethodAsync(
        CreateDepreciationMethodRequest request,
        IDepreciationMethodRepository repository,
        ISequenceGenerator sequenceGenerator,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var methodId = $"{MethodIdPrefix}{nextSequence:D6}";

        var depreciationMethod = await repository.CreateAsync(
            request.ToEntity(methodId),
            cancellationToken);

        return Results.Created($"/api/depreciation-methods/{depreciationMethod.Id}", DepreciationMethodResponse.FromEntity(depreciationMethod));
    }

    private static async Task<IResult> UpdateDepreciationMethodAsync(
        string id,
        UpdateDepreciationMethodRequest request,
        IDepreciationMethodRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid depreciation method id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var depreciationMethod = await repository.GetByIdAsync(id, cancellationToken);
        if (depreciationMethod is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(depreciationMethod);

        var updated = await repository.UpdateAsync(id, depreciationMethod, cancellationToken);
        return updated ? Results.Ok(DepreciationMethodResponse.FromEntity(depreciationMethod)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteDepreciationMethodAsync(
        string id,
        IDepreciationMethodRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid depreciation method id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
