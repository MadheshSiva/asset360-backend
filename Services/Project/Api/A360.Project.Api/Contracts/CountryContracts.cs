using CountryEntity = A360.Project.Domain.Entities.Country;

namespace A360.Project.Api.Contracts;

public sealed record CreateCountryRequest(
    string? ProjectId,
    string? CountryName,
    string? Description,
    string? TimeZone,
    string? CountryCode,
    string? Latitude,
    string? Longitude,
    bool Status,
    string? CreatedBy,
    string? ClientId,
    string? TenantId);

public sealed record UpdateCountryRequest(
    string? CountryName,
    string? Description,
    string? TimeZone,
    string? CountryCode,
    string? Latitude,
    string? Longitude,
    bool Status,
    string? ClientId,
    string? UpdatedBy);

public sealed record CountryResponse(
    string Id,
    string ProjectId,
    string CountryName,
    string Description,
    string TimeZone,
    string CountryCode,
    string Latitude,
    string Longitude,
    bool Status,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static CountryResponse FromEntity(CountryEntity country)
    {
        return new CountryResponse(
            country.Id,
            country.ProjectId,
            country.CountryName,
            country.Description,
            country.TimeZone,
            country.CountryCode,
            country.Latitude,
            country.Longitude,
            country.Status,
            country.CreatedBy,
            country.CreatedAt,
            country.UpdatedBy,
            country.UpdatedAt,
            country.ClientId,
            country.TenantId,
            country.IsDeleted);
    }
}
