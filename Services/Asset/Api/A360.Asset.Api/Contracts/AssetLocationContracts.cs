using AssetLocationEntity = A360.Asset.Domain.Entities.AssetLocation;

namespace A360.Asset.Api.Contracts;

public sealed record CreateAssetLocationRequest(
    string? AssetId,
    string? AssetName,
    string? CurrentLocation,
    string? GpsCoordinates,
    string? LocationHistory,
    string? ZoneTransitions,
    string? LastSeenLocation,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetLocationEntity ToEntity()
    {
        return new AssetLocationEntity
        {
            AssetId = AssetId ?? string.Empty,
            AssetName = AssetName ?? string.Empty,
            CurrentLocation = CurrentLocation,
            GpsCoordinates = GpsCoordinates,
            LocationHistory = LocationHistory,
            ZoneTransitions = ZoneTransitions,
            LastSeenLocation = LastSeenLocation,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssetLocationRequest(
    string? AssetName,
    string? CurrentLocation,
    string? GpsCoordinates,
    string? LocationHistory,
    string? ZoneTransitions,
    string? LastSeenLocation,
    string? UpdatedBy)
{
    public void ApplyTo(AssetLocationEntity location)
    {
        location.AssetName = AssetName ?? string.Empty;
        location.CurrentLocation = CurrentLocation;
        location.GpsCoordinates = GpsCoordinates;
        location.LocationHistory = LocationHistory;
        location.ZoneTransitions = ZoneTransitions;
        location.LastSeenLocation = LastSeenLocation;
        location.UpdatedBy = UpdatedBy;
        location.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetLocationResponse(
    string Id,
    string AssetId,
    string AssetName,
    string? CurrentLocation,
    string? GpsCoordinates,
    string? LocationHistory,
    string? ZoneTransitions,
    string? LastSeenLocation,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssetLocationResponse FromEntity(AssetLocationEntity location)
    {
        return new AssetLocationResponse(
            location.Id,
            location.AssetId,
            location.AssetName,
            location.CurrentLocation,
            location.GpsCoordinates,
            location.LocationHistory,
            location.ZoneTransitions,
            location.LastSeenLocation,
            location.CreatedBy,
            location.CreatedAt,
            location.UpdatedBy,
            location.UpdatedAt,
            location.ClientId,
            location.TenantId,
            location.IsDeleted);
    }
}
