using A360.Asset.Repository.Repositories;
using A360.Domain.Entities;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Activity;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class ManufacturerEndpoints
{
    private const string SequenceName = "manufacturer";
    private const string ManufacturerCodePrefix = "MFR";

    public static RouteGroupBuilder MapManufacturerEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/manufacturers").WithTags("Manufacturers");

        group.MapGet("", GetManufacturersAsync).WithName("GetManufacturers");
        group.MapGet("/{id}", GetManufacturerByIdAsync).WithName("GetManufacturerById");
        group.MapPost("", CreateManufacturerAsync).WithName("CreateManufacturer");
        group.MapPut("/{id}", UpdateManufacturerAsync).WithName("UpdateManufacturer");
        group.MapDelete("/{id}", DeleteManufacturerAsync).WithName("DeleteManufacturer");

        return group;
    }

    private static async Task<IResult> GetManufacturersAsync(
        IManufacturerRepository repository,
        CancellationToken cancellationToken)
    {
        var manufacturers = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(manufacturers.Select(ManufacturerResponse.FromEntity));
    }

    private static async Task<IResult> GetManufacturerByIdAsync(
        string id,
        IManufacturerRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid manufacturer id." });
        }

        var manufacturer = await repository.GetByIdAsync(id, cancellationToken);
        if (manufacturer is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("Manufacturer", manufacturer.ManufacturerCode, manufacturer.ManufacturerName, EventAction.Viewed, cancellationToken);

        return Results.Ok(ManufacturerResponse.FromEntity(manufacturer));
    }

    private static async Task<IResult> CreateManufacturerAsync(
        CreateManufacturerRequest request,
        IManufacturerRepository repository,
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
        var manufacturerCode = $"{ManufacturerCodePrefix}{nextSequence:D6}";

        var manufacturer = await repository.CreateAsync(
            request.ToEntity(manufacturerCode, asset.AssetName),
            cancellationToken);

        await eventLogger.LogAsync("Manufacturer", manufacturer.ManufacturerCode, manufacturer.ManufacturerName, EventAction.Created, cancellationToken);

        return Results.Created($"/api/manufacturers/{manufacturer.Id}", ManufacturerResponse.FromEntity(manufacturer));
    }

    private static async Task<IResult> UpdateManufacturerAsync(
        string id,
        UpdateManufacturerRequest request,
        IManufacturerRepository repository,
        IAssetRepository assetRepository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid manufacturer id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var manufacturer = await repository.GetByIdAsync(id, cancellationToken);
        if (manufacturer is null)
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

        request.ApplyTo(manufacturer, asset.AssetName);

        var updated = await repository.UpdateAsync(id, manufacturer, cancellationToken);
        if (updated)
        {
            await eventLogger.LogAsync("Manufacturer", manufacturer.ManufacturerCode, manufacturer.ManufacturerName, EventAction.Updated, cancellationToken);
        }

        return updated ? Results.Ok(ManufacturerResponse.FromEntity(manufacturer)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteManufacturerAsync(
        string id,
        IManufacturerRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid manufacturer id." });
        }

        var manufacturer = await repository.GetByIdAsync(id, cancellationToken);
        if (manufacturer is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (deleted)
        {
            await eventLogger.LogAsync("Manufacturer", manufacturer.ManufacturerCode, manufacturer.ManufacturerName, EventAction.Deleted, cancellationToken);
        }

        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
