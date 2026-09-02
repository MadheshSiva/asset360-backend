using AssetTrackingAndTelemetryEntity = A360.Asset.Domain.Entities.AssetTrackingAndTelemetry;

namespace A360.Asset.Api.Contracts;

public sealed record CreateAssetTrackingAndTelemetryRequest(
    string? AssetId,
    string? AssetName,
    string? DeviceIdentifier,
    string? TagIds,
    string? MovementLogs,
    DateTime? LastSeenTimestamp,
    string? SpeedRoute,
    string? SensorData,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetTrackingAndTelemetryEntity ToEntity(string trackingId)
    {
        return new AssetTrackingAndTelemetryEntity
        {
            TrackingId = trackingId,
            AssetId = AssetId ?? string.Empty,
            AssetName = AssetName ?? string.Empty,
            DeviceIdentifier = DeviceIdentifier ?? string.Empty,
            TagIds = TagIds ?? string.Empty,
            MovementLogs = MovementLogs ?? string.Empty,
            LastSeenTimestamp = LastSeenTimestamp,
            SpeedRoute = SpeedRoute ?? string.Empty,
            SensorData = SensorData ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssetTrackingAndTelemetryRequest(
    string? AssetId,
    string? AssetName,
    string? DeviceIdentifier,
    string? TagIds,
    string? MovementLogs,
    DateTime? LastSeenTimestamp,
    string? SpeedRoute,
    string? SensorData,
    string? UpdatedBy)
{
    public void ApplyTo(AssetTrackingAndTelemetryEntity telemetry)
    {
        telemetry.AssetId = AssetId ?? string.Empty;
        telemetry.AssetName = AssetName ?? string.Empty;
        telemetry.DeviceIdentifier = DeviceIdentifier ?? string.Empty;
        telemetry.TagIds = TagIds ?? string.Empty;
        telemetry.MovementLogs = MovementLogs ?? string.Empty;
        telemetry.LastSeenTimestamp = LastSeenTimestamp;
        telemetry.SpeedRoute = SpeedRoute ?? string.Empty;
        telemetry.SensorData = SensorData ?? string.Empty;
        telemetry.UpdatedBy = UpdatedBy;
        telemetry.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetTrackingAndTelemetryResponse(
    string Id,
    string TrackingId,
    string AssetId,
    string AssetName,
    string DeviceIdentifier,
    string TagIds,
    string MovementLogs,
    DateTime? LastSeenTimestamp,
    string SpeedRoute,
    string SensorData,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssetTrackingAndTelemetryResponse FromEntity(AssetTrackingAndTelemetryEntity telemetry)
    {
        return new AssetTrackingAndTelemetryResponse(
            telemetry.Id,
            telemetry.TrackingId,
            telemetry.AssetId,
            telemetry.AssetName,
            telemetry.DeviceIdentifier,
            telemetry.TagIds,
            telemetry.MovementLogs,
            telemetry.LastSeenTimestamp,
            telemetry.SpeedRoute,
            telemetry.SensorData,
            telemetry.CreatedBy,
            telemetry.CreatedAt,
            telemetry.UpdatedBy,
            telemetry.UpdatedAt,
            telemetry.ClientId,
            telemetry.TenantId,
            telemetry.IsDeleted);
    }
}
