using AssetUtilizationAndPerformanceEntity = A360.Asset.Domain.Entities.AssetUtilizationAndPerformance;

namespace A360.Asset.Api.Contracts;

public sealed record CreateAssetUtilizationAndPerformanceRequest(
    string? AssetId,
    string? AssetName,
    double UsageHours,
    double IdleTime,
    string? MovementFrequency,
    double UtilizationPercentage,
    string? ProductivityMetrics,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetUtilizationAndPerformanceEntity ToEntity(string utilizationPerformanceId)
    {
        return new AssetUtilizationAndPerformanceEntity
        {
            UtilizationPerformanceId = utilizationPerformanceId,
            AssetId = AssetId ?? string.Empty,
            AssetName = AssetName ?? string.Empty,
            UsageHours = UsageHours,
            IdleTime = IdleTime,
            MovementFrequency = MovementFrequency ?? string.Empty,
            UtilizationPercentage = UtilizationPercentage,
            ProductivityMetrics = ProductivityMetrics ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssetUtilizationAndPerformanceRequest(
    string? AssetId,
    string? AssetName,
    double UsageHours,
    double IdleTime,
    string? MovementFrequency,
    double UtilizationPercentage,
    string? ProductivityMetrics,
    string? UpdatedBy)
{
    public void ApplyTo(AssetUtilizationAndPerformanceEntity utilizationPerformance)
    {
        utilizationPerformance.AssetId = AssetId ?? string.Empty;
        utilizationPerformance.AssetName = AssetName ?? string.Empty;
        utilizationPerformance.UsageHours = UsageHours;
        utilizationPerformance.IdleTime = IdleTime;
        utilizationPerformance.MovementFrequency = MovementFrequency ?? string.Empty;
        utilizationPerformance.UtilizationPercentage = UtilizationPercentage;
        utilizationPerformance.ProductivityMetrics = ProductivityMetrics ?? string.Empty;
        utilizationPerformance.UpdatedBy = UpdatedBy;
        utilizationPerformance.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetUtilizationAndPerformanceResponse(
    string Id,
    string UtilizationPerformanceId,
    string AssetId,
    string AssetName,
    double UsageHours,
    double IdleTime,
    string MovementFrequency,
    double UtilizationPercentage,
    string ProductivityMetrics,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssetUtilizationAndPerformanceResponse FromEntity(AssetUtilizationAndPerformanceEntity utilizationPerformance)
    {
        return new AssetUtilizationAndPerformanceResponse(
            utilizationPerformance.Id,
            utilizationPerformance.UtilizationPerformanceId,
            utilizationPerformance.AssetId,
            utilizationPerformance.AssetName,
            utilizationPerformance.UsageHours,
            utilizationPerformance.IdleTime,
            utilizationPerformance.MovementFrequency,
            utilizationPerformance.UtilizationPercentage,
            utilizationPerformance.ProductivityMetrics,
            utilizationPerformance.CreatedBy,
            utilizationPerformance.CreatedAt,
            utilizationPerformance.UpdatedBy,
            utilizationPerformance.UpdatedAt,
            utilizationPerformance.ClientId,
            utilizationPerformance.TenantId,
            utilizationPerformance.IsDeleted);
    }
}
