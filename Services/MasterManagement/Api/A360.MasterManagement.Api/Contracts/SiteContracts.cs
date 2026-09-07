using SiteEntity = A360.MasterManagement.Domain.Entities.Site;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateSiteRequest(
    string? AssetId,
    string? SiteName,
    string? Organization,
    string? BusinessUnit,
    string? SiteType,
    string? Address,
    string? Country,
    string? State,
    string? City,
    double? GpsLatitude,
    double? GpsLongitude,
    string? SiteManager,
    string? ContactDetails,
    string? OperatingHours,
    string? Status,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public SiteEntity ToEntity(string siteCode, string assetName)
    {
        return new SiteEntity
        {
            SiteCode = siteCode,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            SiteName = SiteName ?? string.Empty,
            Organization = Organization ?? string.Empty,
            BusinessUnit = BusinessUnit ?? string.Empty,
            SiteType = SiteType ?? string.Empty,
            Address = Address ?? string.Empty,
            Country = Country ?? string.Empty,
            State = State ?? string.Empty,
            City = City ?? string.Empty,
            GpsLatitude = GpsLatitude,
            GpsLongitude = GpsLongitude,
            SiteManager = SiteManager ?? string.Empty,
            ContactDetails = ContactDetails ?? string.Empty,
            OperatingHours = OperatingHours ?? string.Empty,
            Status = Status,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateSiteRequest(
    string? AssetId,
    string? SiteName,
    string? Organization,
    string? BusinessUnit,
    string? SiteType,
    string? Address,
    string? Country,
    string? State,
    string? City,
    double? GpsLatitude,
    double? GpsLongitude,
    string? SiteManager,
    string? ContactDetails,
    string? OperatingHours,
    string? Status,
    string? UpdatedBy)
{
    public void ApplyTo(SiteEntity site, string assetName)
    {
        site.AssetId = AssetId ?? string.Empty;
        site.AssetName = assetName;
        site.SiteName = SiteName ?? string.Empty;
        site.Organization = Organization ?? string.Empty;
        site.BusinessUnit = BusinessUnit ?? string.Empty;
        site.SiteType = SiteType ?? string.Empty;
        site.Address = Address ?? string.Empty;
        site.Country = Country ?? string.Empty;
        site.State = State ?? string.Empty;
        site.City = City ?? string.Empty;
        site.GpsLatitude = GpsLatitude;
        site.GpsLongitude = GpsLongitude;
        site.SiteManager = SiteManager ?? string.Empty;
        site.ContactDetails = ContactDetails ?? string.Empty;
        site.OperatingHours = OperatingHours ?? string.Empty;
        site.Status = Status;
        site.UpdatedBy = UpdatedBy;
        site.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record SiteResponse(
    string Id,
    string SiteCode,
    string AssetId,
    string AssetName,
    string SiteName,
    string Organization,
    string BusinessUnit,
    string SiteType,
    string Address,
    string Country,
    string State,
    string City,
    double? GpsLatitude,
    double? GpsLongitude,
    string SiteManager,
    string ContactDetails,
    string OperatingHours,
    string? Status,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static SiteResponse FromEntity(SiteEntity site)
    {
        return new SiteResponse(
            site.Id,
            site.SiteCode,
            site.AssetId,
            site.AssetName,
            site.SiteName,
            site.Organization,
            site.BusinessUnit,
            site.SiteType,
            site.Address,
            site.Country,
            site.State,
            site.City,
            site.GpsLatitude,
            site.GpsLongitude,
            site.SiteManager,
            site.ContactDetails,
            site.OperatingHours,
            site.Status,
            site.CreatedBy,
            site.CreatedAt,
            site.UpdatedBy,
            site.UpdatedAt,
            site.ClientId,
            site.TenantId,
            site.IsDeleted);
    }
}
