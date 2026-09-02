using A360.Asset.Repository.Repositories;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class AlertTypeEndpoints
{
    private const string SequenceName = "alert_type";
    private const string AlertTypeIdPrefix = "ALT";

    public static RouteGroupBuilder MapAlertTypeEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/alert-types").WithTags("AlertTypes");

        group.MapGet("", GetAlertTypesAsync).WithName("GetAlertTypes");
        group.MapGet("/{id}", GetAlertTypeByIdAsync).WithName("GetAlertTypeById");
        group.MapPost("", CreateAlertTypeAsync).WithName("CreateAlertType");
        group.MapPut("/{id}", UpdateAlertTypeAsync).WithName("UpdateAlertType");
        group.MapDelete("/{id}", DeleteAlertTypeAsync).WithName("DeleteAlertType");

        return group;
    }

    private static async Task<IResult> GetAlertTypesAsync(
        IAlertTypeRepository repository,
        CancellationToken cancellationToken)
    {
        var alertTypes = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(alertTypes.Select(AlertTypeResponse.FromEntity));
    }

    private static async Task<IResult> GetAlertTypeByIdAsync(
        string id,
        IAlertTypeRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid alert type id." });
        }

        var alertType = await repository.GetByIdAsync(id, cancellationToken);
        return alertType is null ? Results.NotFound() : Results.Ok(AlertTypeResponse.FromEntity(alertType));
    }

    private static async Task<IResult> CreateAlertTypeAsync(
        CreateAlertTypeRequest request,
        IAlertTypeRepository repository,
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
        var alertTypeId = $"{AlertTypeIdPrefix}{nextSequence:D6}";

        var alertType = await repository.CreateAsync(
            request.ToEntity(alertTypeId, asset.AssetName),
            cancellationToken);

        return Results.Created($"/api/alert-types/{alertType.Id}", AlertTypeResponse.FromEntity(alertType));
    }

    private static async Task<IResult> UpdateAlertTypeAsync(
        string id,
        UpdateAlertTypeRequest request,
        IAlertTypeRepository repository,
        IAssetRepository assetRepository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid alert type id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var alertType = await repository.GetByIdAsync(id, cancellationToken);
        if (alertType is null)
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

        request.ApplyTo(alertType, asset.AssetName);

        var updated = await repository.UpdateAsync(id, alertType, cancellationToken);
        return updated ? Results.Ok(AlertTypeResponse.FromEntity(alertType)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteAlertTypeAsync(
        string id,
        IAlertTypeRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid alert type id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
