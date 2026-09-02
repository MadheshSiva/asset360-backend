using A360.Asset.Repository.Repositories;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class AssignedCustodianEndpoints
{
    private const string SequenceName = "assigned_custodian";
    private const string AssignedCustodianIdPrefix = "ACU";

    public static RouteGroupBuilder MapAssignedCustodianEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/assigned-custodians").WithTags("AssignedCustodians");

        group.MapGet("", GetAssignedCustodiansAsync).WithName("GetAssignedCustodians");
        group.MapGet("/{id}", GetAssignedCustodianByIdAsync).WithName("GetAssignedCustodianById");
        group.MapPost("", CreateAssignedCustodianAsync).WithName("CreateAssignedCustodian");
        group.MapPut("/{id}", UpdateAssignedCustodianAsync).WithName("UpdateAssignedCustodian");
        group.MapDelete("/{id}", DeleteAssignedCustodianAsync).WithName("DeleteAssignedCustodian");

        return group;
    }

    private static async Task<IResult> GetAssignedCustodiansAsync(
        IAssignedCustodianRepository repository,
        CancellationToken cancellationToken)
    {
        var assignedCustodians = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(assignedCustodians.Select(AssignedCustodianResponse.FromEntity));
    }

    private static async Task<IResult> GetAssignedCustodianByIdAsync(
        string id,
        IAssignedCustodianRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid assigned custodian id." });
        }

        var assignedCustodian = await repository.GetByIdAsync(id, cancellationToken);
        return assignedCustodian is null ? Results.NotFound() : Results.Ok(AssignedCustodianResponse.FromEntity(assignedCustodian));
    }

    private static async Task<IResult> CreateAssignedCustodianAsync(
        CreateAssignedCustodianRequest request,
        IAssignedCustodianRepository repository,
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
        var assignedCustodianId = $"{AssignedCustodianIdPrefix}{nextSequence:D6}";

        var assignedCustodian = await repository.CreateAsync(
            request.ToEntity(assignedCustodianId, asset.AssetName),
            cancellationToken);

        return Results.Created($"/api/assigned-custodians/{assignedCustodian.Id}", AssignedCustodianResponse.FromEntity(assignedCustodian));
    }

    private static async Task<IResult> UpdateAssignedCustodianAsync(
        string id,
        UpdateAssignedCustodianRequest request,
        IAssignedCustodianRepository repository,
        IAssetRepository assetRepository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid assigned custodian id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var assignedCustodian = await repository.GetByIdAsync(id, cancellationToken);
        if (assignedCustodian is null)
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

        request.ApplyTo(assignedCustodian, asset.AssetName);

        var updated = await repository.UpdateAsync(id, assignedCustodian, cancellationToken);
        return updated ? Results.Ok(AssignedCustodianResponse.FromEntity(assignedCustodian)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteAssignedCustodianAsync(
        string id,
        IAssignedCustodianRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid assigned custodian id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
