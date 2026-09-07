using A360.Asset.Repository.Repositories;
using A360.Domain.Entities;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Activity;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class ShiftMasterEndpoints
{
    private const string SequenceName = "shift_master";
    private const string ShiftIdPrefix = "SFM";

    public static RouteGroupBuilder MapShiftMasterEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/shift-masters").WithTags("ShiftMasters");

        group.MapGet("", GetShiftMastersAsync).WithName("GetShiftMasters");
        group.MapGet("/{id}", GetShiftMasterByIdAsync).WithName("GetShiftMasterById");
        group.MapPost("", CreateShiftMasterAsync).WithName("CreateShiftMaster");
        group.MapPut("/{id}", UpdateShiftMasterAsync).WithName("UpdateShiftMaster");
        group.MapDelete("/{id}", DeleteShiftMasterAsync).WithName("DeleteShiftMaster");

        return group;
    }

    private static async Task<IResult> GetShiftMastersAsync(
        IShiftMasterRepository repository,
        CancellationToken cancellationToken)
    {
        var shiftMasters = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(shiftMasters.Select(ShiftMasterResponse.FromEntity));
    }

    private static async Task<IResult> GetShiftMasterByIdAsync(
        string id,
        IShiftMasterRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid shift master id." });
        }

        var shiftMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (shiftMaster is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("ShiftMaster", shiftMaster.ShiftId, shiftMaster.ShiftName, EventAction.Viewed, cancellationToken);

        return Results.Ok(ShiftMasterResponse.FromEntity(shiftMaster));
    }

    private static async Task<IResult> CreateShiftMasterAsync(
        CreateShiftMasterRequest request,
        IShiftMasterRepository repository,
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
        var shiftId = $"{ShiftIdPrefix}{nextSequence:D6}";

        var shiftMaster = await repository.CreateAsync(
            request.ToEntity(shiftId, asset.AssetName),
            cancellationToken);

        await eventLogger.LogAsync("ShiftMaster", shiftMaster.ShiftId, shiftMaster.ShiftName, EventAction.Created, cancellationToken);

        return Results.Created($"/api/shift-masters/{shiftMaster.Id}", ShiftMasterResponse.FromEntity(shiftMaster));
    }

    private static async Task<IResult> UpdateShiftMasterAsync(
        string id,
        UpdateShiftMasterRequest request,
        IShiftMasterRepository repository,
        IAssetRepository assetRepository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid shift master id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var shiftMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (shiftMaster is null)
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

        request.ApplyTo(shiftMaster, asset.AssetName);

        var updated = await repository.UpdateAsync(id, shiftMaster, cancellationToken);
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("ShiftMaster", shiftMaster.ShiftId, shiftMaster.ShiftName, EventAction.Updated, cancellationToken);

        return Results.Ok(ShiftMasterResponse.FromEntity(shiftMaster));
    }

    private static async Task<IResult> DeleteShiftMasterAsync(
        string id,
        IShiftMasterRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid shift master id." });
        }

        var shiftMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (shiftMaster is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("ShiftMaster", shiftMaster.ShiftId, shiftMaster.ShiftName, EventAction.Deleted, cancellationToken);

        return Results.NoContent();
    }
}
