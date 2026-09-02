using ChartTypeMasterEntity = A360.MasterManagement.Domain.Entities.ChartTypeMaster;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateChartTypeMasterRequest(
    string? AssetId,
    string? WidgetName,
    string? ConfigJson,
    bool IsActive,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public ChartTypeMasterEntity ToEntity(string widgetId, string assetName)
    {
        return new ChartTypeMasterEntity
        {
            WidgetId = widgetId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            WidgetName = WidgetName ?? string.Empty,
            ConfigJson = ConfigJson ?? string.Empty,
            IsActive = IsActive,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateChartTypeMasterRequest(
    string? AssetId,
    string? WidgetName,
    string? ConfigJson,
    bool IsActive,
    string? UpdatedBy)
{
    public void ApplyTo(ChartTypeMasterEntity chartTypeMaster, string assetName)
    {
        chartTypeMaster.AssetId = AssetId ?? string.Empty;
        chartTypeMaster.AssetName = assetName;
        chartTypeMaster.WidgetName = WidgetName ?? string.Empty;
        chartTypeMaster.ConfigJson = ConfigJson ?? string.Empty;
        chartTypeMaster.IsActive = IsActive;
        chartTypeMaster.UpdatedBy = UpdatedBy;
        chartTypeMaster.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record ChartTypeMasterResponse(
    string Id,
    string WidgetId,
    string AssetId,
    string AssetName,
    string WidgetName,
    string ConfigJson,
    bool IsActive,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static ChartTypeMasterResponse FromEntity(ChartTypeMasterEntity chartTypeMaster)
    {
        return new ChartTypeMasterResponse(
            chartTypeMaster.Id,
            chartTypeMaster.WidgetId,
            chartTypeMaster.AssetId,
            chartTypeMaster.AssetName,
            chartTypeMaster.WidgetName,
            chartTypeMaster.ConfigJson,
            chartTypeMaster.IsActive,
            chartTypeMaster.CreatedBy,
            chartTypeMaster.CreatedAt,
            chartTypeMaster.UpdatedBy,
            chartTypeMaster.UpdatedAt,
            chartTypeMaster.ClientId,
            chartTypeMaster.TenantId,
            chartTypeMaster.IsDeleted);
    }
}
