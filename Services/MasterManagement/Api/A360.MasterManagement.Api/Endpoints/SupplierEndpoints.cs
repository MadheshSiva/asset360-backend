using A360.Asset.Repository.Repositories;
using A360.Domain.Entities;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Activity;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class SupplierEndpoints
{
    private const string SequenceName = "supplier";
    private const string SupplierCodePrefix = "SUP";

    public static RouteGroupBuilder MapSupplierEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/suppliers").WithTags("Suppliers");

        group.MapGet("", GetSuppliersAsync).WithName("GetSuppliers");
        group.MapGet("/{id}", GetSupplierByIdAsync).WithName("GetSupplierById");
        group.MapPost("", CreateSupplierAsync).WithName("CreateSupplier");
        group.MapPut("/{id}", UpdateSupplierAsync).WithName("UpdateSupplier");
        group.MapDelete("/{id}", DeleteSupplierAsync).WithName("DeleteSupplier");

        return group;
    }

    private static async Task<IResult> GetSuppliersAsync(
        ISupplierRepository repository,
        CancellationToken cancellationToken)
    {
        var suppliers = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(suppliers.Select(SupplierResponse.FromEntity));
    }

    private static async Task<IResult> GetSupplierByIdAsync(
        string id,
        ISupplierRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid supplier id." });
        }

        var supplier = await repository.GetByIdAsync(id, cancellationToken);
        if (supplier is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("Supplier", supplier.SupplierCode, supplier.SupplierName, EventAction.Viewed, cancellationToken);

        return Results.Ok(SupplierResponse.FromEntity(supplier));
    }

    private static async Task<IResult> CreateSupplierAsync(
        CreateSupplierRequest request,
        ISupplierRepository repository,
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
        var supplierCode = $"{SupplierCodePrefix}{nextSequence:D6}";

        var supplier = await repository.CreateAsync(
            request.ToEntity(supplierCode, asset.AssetName),
            cancellationToken);

        await eventLogger.LogAsync("Supplier", supplier.SupplierCode, supplier.SupplierName, EventAction.Created, cancellationToken);

        return Results.Created($"/api/suppliers/{supplier.Id}", SupplierResponse.FromEntity(supplier));
    }

    private static async Task<IResult> UpdateSupplierAsync(
        string id,
        UpdateSupplierRequest request,
        ISupplierRepository repository,
        IAssetRepository assetRepository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid supplier id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var supplier = await repository.GetByIdAsync(id, cancellationToken);
        if (supplier is null)
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

        request.ApplyTo(supplier, asset.AssetName);

        var updated = await repository.UpdateAsync(id, supplier, cancellationToken);
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("Supplier", supplier.SupplierCode, supplier.SupplierName, EventAction.Updated, cancellationToken);

        return Results.Ok(SupplierResponse.FromEntity(supplier));
    }

    private static async Task<IResult> DeleteSupplierAsync(
        string id,
        ISupplierRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid supplier id." });
        }

        var supplier = await repository.GetByIdAsync(id, cancellationToken);
        if (supplier is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("Supplier", supplier.SupplierCode, supplier.SupplierName, EventAction.Deleted, cancellationToken);

        return Results.NoContent();
    }
}
