using CurrentLocationEntity = A360.MasterManagement.Domain.Entities.CurrentLocation;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateCurrentLocationRequest(
    string? CurrentLocationName,
    bool Active,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public CurrentLocationEntity ToEntity(string locationId)
    {
        return new CurrentLocationEntity
        {
            LocationId = locationId,
            CurrentLocationName = CurrentLocationName ?? string.Empty,
            Active = Active,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateCurrentLocationRequest(
    string? CurrentLocationName,
    bool Active,
    string? UpdatedBy)
{
    public void ApplyTo(CurrentLocationEntity currentLocation)
    {
        currentLocation.CurrentLocationName = CurrentLocationName ?? string.Empty;
        currentLocation.Active = Active;
        currentLocation.UpdatedBy = UpdatedBy;
        currentLocation.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record CurrentLocationResponse(
    string Id,
    string LocationId,
    string CurrentLocationName,
    bool Active,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static CurrentLocationResponse FromEntity(CurrentLocationEntity currentLocation)
    {
        return new CurrentLocationResponse(
            currentLocation.Id,
            currentLocation.LocationId,
            currentLocation.CurrentLocationName,
            currentLocation.Active,
            currentLocation.CreatedBy,
            currentLocation.CreatedAt,
            currentLocation.UpdatedBy,
            currentLocation.UpdatedAt,
            currentLocation.ClientId,
            currentLocation.TenantId,
            currentLocation.IsDeleted);
    }
}
