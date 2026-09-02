using A360.Asset.Repository.Repositories;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class MasterMaintenanceEndpoints
{
    private const string SequenceName = "master_maintenance";
    private const string MasterMaintenanceIdPrefix = "MMT";

    public static RouteGroupBuilder MapMasterMaintenanceEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/master-maintenances").WithTags("MasterMaintenances");

        group.MapGet("", GetMasterMaintenancesAsync).WithName("GetMasterMaintenances");
        group.MapGet("/{id}", GetMasterMaintenanceByIdAsync).WithName("GetMasterMaintenanceById");
        group.MapPost("", CreateMasterMaintenanceAsync).WithName("CreateMasterMaintenance");
        group.MapPut("/{id}", UpdateMasterMaintenanceAsync).WithName("UpdateMasterMaintenance");
        group.MapDelete("/{id}", DeleteMasterMaintenanceAsync).WithName("DeleteMasterMaintenance");

        return group;
    }

    private static async Task<IResult> GetMasterMaintenancesAsync(
        IMasterMaintenanceRepository repository,
        CancellationToken cancellationToken)
    {
        var masterMaintenances = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(masterMaintenances.Select(MasterMaintenanceResponse.FromEntity));
    }

    private static async Task<IResult> GetMasterMaintenanceByIdAsync(
        string id,
        IMasterMaintenanceRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid master maintenance id." });
        }

        var masterMaintenance = await repository.GetByIdAsync(id, cancellationToken);
        return masterMaintenance is null ? Results.NotFound() : Results.Ok(MasterMaintenanceResponse.FromEntity(masterMaintenance));
    }

    private static async Task<IResult> CreateMasterMaintenanceAsync(
        CreateMasterMaintenanceRequest request,
        IMasterMaintenanceRepository repository,
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
        var masterMaintenanceId = $"{MasterMaintenanceIdPrefix}{nextSequence:D6}";

        var masterMaintenance = await repository.CreateAsync(
            request.ToEntity(masterMaintenanceId, asset.AssetName),
            cancellationToken);

        return Results.Created($"/api/master-maintenances/{masterMaintenance.Id}", MasterMaintenanceResponse.FromEntity(masterMaintenance));
    }

    private static async Task<IResult> UpdateMasterMaintenanceAsync(
        string id,
        UpdateMasterMaintenanceRequest request,
        IMasterMaintenanceRepository repository,
        IAssetRepository assetRepository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid master maintenance id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var masterMaintenance = await repository.GetByIdAsync(id, cancellationToken);
        if (masterMaintenance is null)
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

        request.ApplyTo(masterMaintenance, asset.AssetName);

        var updated = await repository.UpdateAsync(id, masterMaintenance, cancellationToken);
        return updated ? Results.Ok(MasterMaintenanceResponse.FromEntity(masterMaintenance)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteMasterMaintenanceAsync(
        string id,
        IMasterMaintenanceRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid master maintenance id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
