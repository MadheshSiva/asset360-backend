using A360.Asset.Repository.Repositories;
using A360.Domain.Entities;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Activity;
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
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid resource type id." });
        }

        var resourceType = await repository.GetByIdAsync(id, cancellationToken);
        if (resourceType is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("ResourceType", resourceType.TypeId, resourceType.TypeName, EventAction.Viewed, cancellationToken);

        return Results.Ok(ResourceTypeResponse.FromEntity(resourceType));
    }

    private static async Task<IResult> CreateResourceTypeAsync(
        CreateResourceTypeRequest request,
        IResourceTypeRepository repository,
        IAssetRepository assetRepository,
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

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var typeId = $"{TypeIdPrefix}{nextSequence:D6}";

        var resourceType = await repository.CreateAsync(
            request.ToEntity(typeId, asset.AssetName),
            cancellationToken);

        await eventLogger.LogAsync("ResourceType", resourceType.TypeId, resourceType.TypeName, EventAction.Created, cancellationToken);

        return Results.Created($"/api/resource-types/{resourceType.Id}", ResourceTypeResponse.FromEntity(resourceType));
    }

    private static async Task<IResult> UpdateResourceTypeAsync(
        string id,
        UpdateResourceTypeRequest request,
        IResourceTypeRepository repository,
        IAssetRepository assetRepository,
        IEventLogger eventLogger,
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
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("ResourceType", resourceType.TypeId, resourceType.TypeName, EventAction.Updated, cancellationToken);

        return Results.Ok(ResourceTypeResponse.FromEntity(resourceType));
    }

    private static async Task<IResult> DeleteResourceTypeAsync(
        string id,
        IResourceTypeRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid resource type id." });
        }

        var resourceType = await repository.GetByIdAsync(id, cancellationToken);
        if (resourceType is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("ResourceType", resourceType.TypeId, resourceType.TypeName, EventAction.Deleted, cancellationToken);

        return Results.NoContent();
    }
}
