using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.Asset.Api.Endpoints;

public static class AssetMaintenanceAndServiceEndpoints
{
    private const string SequenceName = "asset-maintenance-and-service";
    private const string MaintenanceServiceIdPrefix = "AMS";

    public static RouteGroupBuilder MapAssetMaintenanceAndServiceEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/asset-maintenance-and-services").WithTags("AssetMaintenanceAndServices");

        group.MapGet("", GetAssetMaintenanceAndServicesAsync).WithName("GetAssetMaintenanceAndServices");
        group.MapGet("/{id}", GetAssetMaintenanceAndServiceByIdAsync).WithName("GetAssetMaintenanceAndServiceById");
        group.MapGet("/by-asset/{assetId}", GetAssetMaintenanceAndServicesByAssetIdAsync).WithName("GetAssetMaintenanceAndServicesByAssetId");
        group.MapPost("", CreateAssetMaintenanceAndServiceAsync).WithName("CreateAssetMaintenanceAndService");
        group.MapPut("/{id}", UpdateAssetMaintenanceAndServiceAsync).WithName("UpdateAssetMaintenanceAndService");
        group.MapDelete("/{id}", DeleteAssetMaintenanceAndServiceAsync).WithName("DeleteAssetMaintenanceAndService");

        return group;
    }

    private static async Task<IResult> GetAssetMaintenanceAndServicesAsync(
        IAssetMaintenanceAndServiceRepository repository,
        CancellationToken cancellationToken)
    {
        var services = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(services.Select(AssetMaintenanceAndServiceResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetMaintenanceAndServiceByIdAsync(
        string id,
        IAssetMaintenanceAndServiceRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset maintenance and service id." });
        }

        var service = await repository.GetByIdAsync(id, cancellationToken);
        return service is null ? Results.NotFound() : Results.Ok(AssetMaintenanceAndServiceResponse.FromEntity(service));
    }

    private static async Task<IResult> GetAssetMaintenanceAndServicesByAssetIdAsync(
        string assetId,
        IAssetMaintenanceAndServiceRepository repository,
        CancellationToken cancellationToken)
    {
        var services = await repository.GetByAssetIdAsync(assetId, cancellationToken);
        return Results.Ok(services.Select(AssetMaintenanceAndServiceResponse.FromEntity));
    }

    private static async Task<IResult> CreateAssetMaintenanceAndServiceAsync(
        CreateAssetMaintenanceAndServiceRequest request,
        IAssetMaintenanceAndServiceRepository repository,
        ISequenceGenerator sequenceGenerator,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var maintenanceServiceId = $"{MaintenanceServiceIdPrefix}{nextSequence:D6}";

        var service = await repository.CreateAsync(request.ToEntity(maintenanceServiceId), cancellationToken);
        return Results.Created($"/api/asset-maintenance-and-services/{service.Id}", AssetMaintenanceAndServiceResponse.FromEntity(service));
    }

    private static async Task<IResult> UpdateAssetMaintenanceAndServiceAsync(
        string id,
        UpdateAssetMaintenanceAndServiceRequest request,
        IAssetMaintenanceAndServiceRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset maintenance and service id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var service = await repository.GetByIdAsync(id, cancellationToken);
        if (service is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(service);

        var updated = await repository.UpdateAsync(id, service, cancellationToken);
        return updated ? Results.Ok(AssetMaintenanceAndServiceResponse.FromEntity(service)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteAssetMaintenanceAndServiceAsync(
        string id,
        IAssetMaintenanceAndServiceRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset maintenance and service id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
