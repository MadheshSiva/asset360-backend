using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class CostCenterEndpoints
{
    private const string SequenceName = "cost_center";
    private const string CostCenterIdPrefix = "CC";

    public static RouteGroupBuilder MapCostCenterEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/cost-centers").WithTags("CostCenters");

        group.MapGet("", GetCostCentersAsync).WithName("GetCostCenters");
        group.MapGet("/{id}", GetCostCenterByIdAsync).WithName("GetCostCenterById");
        group.MapPost("", CreateCostCenterAsync).WithName("CreateCostCenter");
        group.MapPut("/{id}", UpdateCostCenterAsync).WithName("UpdateCostCenter");
        group.MapDelete("/{id}", DeleteCostCenterAsync).WithName("DeleteCostCenter");

        return group;
    }

    private static async Task<IResult> GetCostCentersAsync(
        ICostCenterRepository repository,
        CancellationToken cancellationToken)
    {
        var costCenters = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(costCenters.Select(CostCenterResponse.FromEntity));
    }

    private static async Task<IResult> GetCostCenterByIdAsync(
        string id,
        ICostCenterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid cost center id." });
        }

        var costCenter = await repository.GetByIdAsync(id, cancellationToken);
        return costCenter is null ? Results.NotFound() : Results.Ok(CostCenterResponse.FromEntity(costCenter));
    }

    private static async Task<IResult> CreateCostCenterAsync(
        CreateCostCenterRequest request,
        ICostCenterRepository repository,
        ISequenceGenerator sequenceGenerator,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var costCenterId = $"{CostCenterIdPrefix}{nextSequence:D6}";

        var costCenter = await repository.CreateAsync(
            request.ToEntity(costCenterId),
            cancellationToken);

        return Results.Created($"/api/cost-centers/{costCenter.Id}", CostCenterResponse.FromEntity(costCenter));
    }

    private static async Task<IResult> UpdateCostCenterAsync(
        string id,
        UpdateCostCenterRequest request,
        ICostCenterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid cost center id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var costCenter = await repository.GetByIdAsync(id, cancellationToken);
        if (costCenter is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(costCenter);

        var updated = await repository.UpdateAsync(id, costCenter, cancellationToken);
        return updated ? Results.Ok(CostCenterResponse.FromEntity(costCenter)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteCostCenterAsync(
        string id,
        ICostCenterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid cost center id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
