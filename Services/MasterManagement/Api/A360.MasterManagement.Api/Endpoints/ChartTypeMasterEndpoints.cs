using A360.Asset.Repository.Repositories;
using A360.Domain.Entities;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Activity;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class ChartTypeMasterEndpoints
{
    private const string SequenceName = "chart_type_master";
    private const string ChartTypeMasterIdPrefix = "CHM";

    public static RouteGroupBuilder MapChartTypeMasterEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/chart-type-masters").WithTags("ChartTypeMasters");

        group.MapGet("", GetChartTypeMastersAsync).WithName("GetChartTypeMasters");
        group.MapGet("/{id}", GetChartTypeMasterByIdAsync).WithName("GetChartTypeMasterById");
        group.MapPost("", CreateChartTypeMasterAsync).WithName("CreateChartTypeMaster");
        group.MapPut("/{id}", UpdateChartTypeMasterAsync).WithName("UpdateChartTypeMaster");
        group.MapDelete("/{id}", DeleteChartTypeMasterAsync).WithName("DeleteChartTypeMaster");

        return group;
    }

    private static async Task<IResult> GetChartTypeMastersAsync(
        IChartTypeMasterRepository repository,
        CancellationToken cancellationToken)
    {
        var chartTypeMasters = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(chartTypeMasters.Select(ChartTypeMasterResponse.FromEntity));
    }

    private static async Task<IResult> GetChartTypeMasterByIdAsync(
        string id,
        IChartTypeMasterRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid chart type master id." });
        }

        var chartTypeMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (chartTypeMaster is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("ChartTypeMaster", chartTypeMaster.WidgetId, chartTypeMaster.WidgetName, EventAction.Viewed, cancellationToken);

        return Results.Ok(ChartTypeMasterResponse.FromEntity(chartTypeMaster));
    }

    private static async Task<IResult> CreateChartTypeMasterAsync(
        CreateChartTypeMasterRequest request,
        IChartTypeMasterRepository repository,
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
        var widgetId = $"{ChartTypeMasterIdPrefix}{nextSequence:D6}";

        var chartTypeMaster = await repository.CreateAsync(
            request.ToEntity(widgetId, asset.AssetName),
            cancellationToken);

        await eventLogger.LogAsync("ChartTypeMaster", chartTypeMaster.WidgetId, chartTypeMaster.WidgetName, EventAction.Created, cancellationToken);

        return Results.Created($"/api/chart-type-masters/{chartTypeMaster.Id}", ChartTypeMasterResponse.FromEntity(chartTypeMaster));
    }

    private static async Task<IResult> UpdateChartTypeMasterAsync(
        string id,
        UpdateChartTypeMasterRequest request,
        IChartTypeMasterRepository repository,
        IAssetRepository assetRepository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid chart type master id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var chartTypeMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (chartTypeMaster is null)
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

        request.ApplyTo(chartTypeMaster, asset.AssetName);

        var updated = await repository.UpdateAsync(id, chartTypeMaster, cancellationToken);
        if (updated)
        {
            await eventLogger.LogAsync("ChartTypeMaster", chartTypeMaster.WidgetId, chartTypeMaster.WidgetName, EventAction.Updated, cancellationToken);
        }

        return updated ? Results.Ok(ChartTypeMasterResponse.FromEntity(chartTypeMaster)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteChartTypeMasterAsync(
        string id,
        IChartTypeMasterRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid chart type master id." });
        }

        var chartTypeMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (chartTypeMaster is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (deleted)
        {
            await eventLogger.LogAsync("ChartTypeMaster", chartTypeMaster.WidgetId, chartTypeMaster.WidgetName, EventAction.Deleted, cancellationToken);
        }

        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
