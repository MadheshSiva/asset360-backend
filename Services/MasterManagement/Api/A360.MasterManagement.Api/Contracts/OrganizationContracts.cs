using OrganizationEntity = A360.MasterManagement.Domain.Entities.Organization;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateOrganizationRequest(
    string? AssetId,
    string? OrganizationName,
    string? LegalName,
    string? Logo,
    string? Address,
    string? Country,
    string? State,
    string? City,
    string? PostalCode,
    string? ContactPerson,
    string? Email,
    string? PhoneNumber,
    string? TimeZone,
    string? DateFormat,
    string? Currency,
    string? Status,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public OrganizationEntity ToEntity(string organizationCode, string assetName)
    {
        return new OrganizationEntity
        {
            OrganizationCode = organizationCode,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            OrganizationName = OrganizationName ?? string.Empty,
            LegalName = LegalName ?? string.Empty,
            Logo = Logo,
            Address = Address ?? string.Empty,
            Country = Country ?? string.Empty,
            State = State ?? string.Empty,
            City = City ?? string.Empty,
            PostalCode = PostalCode ?? string.Empty,
            ContactPerson = ContactPerson ?? string.Empty,
            Email = Email ?? string.Empty,
            PhoneNumber = PhoneNumber ?? string.Empty,
            TimeZone = TimeZone ?? string.Empty,
            DateFormat = DateFormat ?? string.Empty,
            Currency = Currency ?? string.Empty,
            Status = Status,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateOrganizationRequest(
    string? AssetId,
    string? OrganizationName,
    string? LegalName,
    string? Logo,
    string? Address,
    string? Country,
    string? State,
    string? City,
    string? PostalCode,
    string? ContactPerson,
    string? Email,
    string? PhoneNumber,
    string? TimeZone,
    string? DateFormat,
    string? Currency,
    string? Status,
    string? UpdatedBy)
{
    public void ApplyTo(OrganizationEntity organization, string assetName)
    {
        organization.AssetId = AssetId ?? string.Empty;
        organization.AssetName = assetName;
        organization.OrganizationName = OrganizationName ?? string.Empty;
        organization.LegalName = LegalName ?? string.Empty;
        organization.Logo = Logo;
        organization.Address = Address ?? string.Empty;
        organization.Country = Country ?? string.Empty;
        organization.State = State ?? string.Empty;
        organization.City = City ?? string.Empty;
        organization.PostalCode = PostalCode ?? string.Empty;
        organization.ContactPerson = ContactPerson ?? string.Empty;
        organization.Email = Email ?? string.Empty;
        organization.PhoneNumber = PhoneNumber ?? string.Empty;
        organization.TimeZone = TimeZone ?? string.Empty;
        organization.DateFormat = DateFormat ?? string.Empty;
        organization.Currency = Currency ?? string.Empty;
        organization.Status = Status;
        organization.UpdatedBy = UpdatedBy;
        organization.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record OrganizationResponse(
    string Id,
    string OrganizationCode,
    string AssetId,
    string AssetName,
    string OrganizationName,
    string LegalName,
    string? Logo,
    string Address,
    string Country,
    string State,
    string City,
    string PostalCode,
    string ContactPerson,
    string Email,
    string PhoneNumber,
    string TimeZone,
    string DateFormat,
    string Currency,
    string? Status,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static OrganizationResponse FromEntity(OrganizationEntity organization)
    {
        return new OrganizationResponse(
            organization.Id,
            organization.OrganizationCode,
            organization.AssetId,
            organization.AssetName,
            organization.OrganizationName,
            organization.LegalName,
            organization.Logo,
            organization.Address,
            organization.Country,
            organization.State,
            organization.City,
            organization.PostalCode,
            organization.ContactPerson,
            organization.Email,
            organization.PhoneNumber,
            organization.TimeZone,
            organization.DateFormat,
            organization.Currency,
            organization.Status,
            organization.CreatedBy,
            organization.CreatedAt,
            organization.UpdatedBy,
            organization.UpdatedAt,
            organization.ClientId,
            organization.TenantId,
            organization.IsDeleted);
    }
}
