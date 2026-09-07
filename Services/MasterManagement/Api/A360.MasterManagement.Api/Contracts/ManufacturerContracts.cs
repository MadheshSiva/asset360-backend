using ManufacturerEntity = A360.MasterManagement.Domain.Entities.Manufacturer;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateManufacturerRequest(
    string? AssetId,
    string? ManufacturerName,
    string? ContactPerson,
    string? Email,
    string? Phone,
    string? Address,
    string? Website,
    string? Status,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public ManufacturerEntity ToEntity(string manufacturerCode, string assetName)
    {
        return new ManufacturerEntity
        {
            ManufacturerCode = manufacturerCode,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            ManufacturerName = ManufacturerName ?? string.Empty,
            ContactPerson = ContactPerson ?? string.Empty,
            Email = Email ?? string.Empty,
            Phone = Phone ?? string.Empty,
            Address = Address ?? string.Empty,
            Website = Website ?? string.Empty,
            Status = Status,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateManufacturerRequest(
    string? AssetId,
    string? ManufacturerName,
    string? ContactPerson,
    string? Email,
    string? Phone,
    string? Address,
    string? Website,
    string? Status,
    string? UpdatedBy)
{
    public void ApplyTo(ManufacturerEntity manufacturer, string assetName)
    {
        manufacturer.AssetId = AssetId ?? string.Empty;
        manufacturer.AssetName = assetName;
        manufacturer.ManufacturerName = ManufacturerName ?? string.Empty;
        manufacturer.ContactPerson = ContactPerson ?? string.Empty;
        manufacturer.Email = Email ?? string.Empty;
        manufacturer.Phone = Phone ?? string.Empty;
        manufacturer.Address = Address ?? string.Empty;
        manufacturer.Website = Website ?? string.Empty;
        manufacturer.Status = Status;
        manufacturer.UpdatedBy = UpdatedBy;
        manufacturer.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record ManufacturerResponse(
    string Id,
    string ManufacturerCode,
    string AssetId,
    string AssetName,
    string ManufacturerName,
    string ContactPerson,
    string Email,
    string Phone,
    string Address,
    string Website,
    string? Status,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static ManufacturerResponse FromEntity(ManufacturerEntity manufacturer)
    {
        return new ManufacturerResponse(
            manufacturer.Id,
            manufacturer.ManufacturerCode,
            manufacturer.AssetId,
            manufacturer.AssetName,
            manufacturer.ManufacturerName,
            manufacturer.ContactPerson,
            manufacturer.Email,
            manufacturer.Phone,
            manufacturer.Address,
            manufacturer.Website,
            manufacturer.Status,
            manufacturer.CreatedBy,
            manufacturer.CreatedAt,
            manufacturer.UpdatedBy,
            manufacturer.UpdatedAt,
            manufacturer.ClientId,
            manufacturer.TenantId,
            manufacturer.IsDeleted);
    }
}
