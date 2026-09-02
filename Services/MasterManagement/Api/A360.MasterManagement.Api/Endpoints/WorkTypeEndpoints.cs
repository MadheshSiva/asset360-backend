using A360.Asset.Repository.Repositories;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class WorkTypeEndpoints
{
    private const string SequenceName = "work_type";
    private const string WorkTypeIdPrefix = "WKT";

    public static RouteGroupBuilder MapWorkTypeEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/work-types").WithTags("WorkTypes");

        group.MapGet("", GetWorkTypesAsync).WithName("GetWorkTypes");
        group.MapGet("/{id}", GetWorkTypeByIdAsync).WithName("GetWorkTypeById");
        group.MapPost("", CreateWorkTypeAsync).WithName("CreateWorkType");
        group.MapPut("/{id}", UpdateWorkTypeAsync).WithName("UpdateWorkType");
        group.MapDelete("/{id}", DeleteWorkTypeAsync).WithName("DeleteWorkType");

        return group;
    }

    private static async Task<IResult> GetWorkTypesAsync(
        IWorkTypeRepository repository,
        CancellationToken cancellationToken)
    {
        var workTypes = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(workTypes.Select(WorkTypeResponse.FromEntity));
    }

    private static async Task<IResult> GetWorkTypeByIdAsync(
        string id,
        IWorkTypeRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid work type id." });
        }

        var workType = await repository.GetByIdAsync(id, cancellationToken);
        return workType is null ? Results.NotFound() : Results.Ok(WorkTypeResponse.FromEntity(workType));
    }

    private static async Task<IResult> CreateWorkTypeAsync(
        CreateWorkTypeRequest request,
        IWorkTypeRepository repository,
        IAssetRepository assetRepository,
        ISequenceGenerator sequenceGenerator,
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

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var workTypeId = $"{WorkTypeIdPrefix}{nextSequence:D6}";

        var workType = await repository.CreateAsync(
            request.ToEntity(workTypeId, asset.AssetName),
            cancellationToken);

        return Results.Created($"/api/work-types/{workType.Id}", WorkTypeResponse.FromEntity(workType));
    }

    private static async Task<IResult> UpdateWorkTypeAsync(
        string id,
        UpdateWorkTypeRequest request,
        IWorkTypeRepository repository,
        IAssetRepository assetRepository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid work type id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var workType = await repository.GetByIdAsync(id, cancellationToken);
        if (workType is null)
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

        request.ApplyTo(workType, asset.AssetName);

        var updated = await repository.UpdateAsync(id, workType, cancellationToken);
        return updated ? Results.Ok(WorkTypeResponse.FromEntity(workType)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteWorkTypeAsync(
        string id,
        IWorkTypeRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid work type id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
