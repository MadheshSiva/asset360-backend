using A360.Asset.Repository.Repositories;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class ResourceTypeEndpoints
{
    private const string SequenceName = "resource_type";
    private const string TypeIdPrefix = "RST";

    public static RouteGroupBuilder MapResourceTypeEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/resource-types").WithTags("ResourceTypes");

        group.MapGet("", GetResourceTypesAsync).WithName("GetResourceTypes");
        group.MapGet("/{id}", GetResourceTypeByIdAsync).WithName("GetResourceTypeById");
        group.MapPost("", CreateResourceTypeAsync).WithName("CreateResourceType");
        group.MapPut("/{id}", UpdateResourceTypeAsync).WithName("UpdateResourceType");
        group.MapDelete("/{id}", DeleteResourceTypeAsync).WithName("DeleteResourceType");

        return group;
    }

    private static async Task<IResult> GetResourceTypesAsync(
        IResourceTypeRepository repository,
        CancellationToken cancellationToken)
    {
        var resourceTypes = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(resourceTypes.Select(ResourceTypeResponse.FromEntity));
    }

    private static async Task<IResult> GetResourceTypeByIdAsync(
        string id,
        IResourceTypeRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid resource type id." });
        }

        var resourceType = await repository.GetByIdAsync(id, cancellationToken);
        return resourceType is null ? Results.NotFound() : Results.Ok(ResourceTypeResponse.FromEntity(resourceType));
    }

    private static async Task<IResult> CreateResourceTypeAsync(
        CreateResourceTypeRequest request,
        IResourceTypeRepository repository,
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
        var typeId = $"{TypeIdPrefix}{nextSequence:D6}";

        var resourceType = await repository.CreateAsync(
            request.ToEntity(typeId, asset.AssetName),
            cancellationToken);

        return Results.Created($"/api/resource-types/{resourceType.Id}", ResourceTypeResponse.FromEntity(resourceType));
    }

    private static async Task<IResult> UpdateResourceTypeAsync(
        string id,
        UpdateResourceTypeRequest request,
        IResourceTypeRepository repository,
        IAssetRepository assetRepository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid resource type id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var resourceType = await repository.GetByIdAsync(id, cancellationToken);
        if (resourceType is null)
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

        request.ApplyTo(resourceType, asset.AssetName);

        var updated = await repository.UpdateAsync(id, resourceType, cancellationToken);
        return updated ? Results.Ok(ResourceTypeResponse.FromEntity(resourceType)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteResourceTypeAsync(
        string id,
        IResourceTypeRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid resource type id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
