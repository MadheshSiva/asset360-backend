using BusinessUnitEntity = A360.MasterManagement.Domain.Entities.BusinessUnit;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateBusinessUnitRequest(
    string? AssetId,
    string? BusinessUnitName,
    string? Organization,
    string? Description,
    string? BusinessUnitHead,
    string? Email,
    string? Phone,
    string? Status,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public BusinessUnitEntity ToEntity(string businessUnitCode, string assetName)
    {
        return new BusinessUnitEntity
        {
            BusinessUnitCode = businessUnitCode,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            BusinessUnitName = BusinessUnitName ?? string.Empty,
            Organization = Organization ?? string.Empty,
            Description = Description ?? string.Empty,
            BusinessUnitHead = BusinessUnitHead ?? string.Empty,
            Email = Email ?? string.Empty,
            Phone = Phone ?? string.Empty,
            Status = Status,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateBusinessUnitRequest(
    string? AssetId,
    string? BusinessUnitName,
    string? Organization,
    string? Description,
    string? BusinessUnitHead,
    string? Email,
    string? Phone,
    string? Status,
    string? UpdatedBy)
{
    public void ApplyTo(BusinessUnitEntity businessUnit, string assetName)
    {
        businessUnit.AssetId = AssetId ?? string.Empty;
        businessUnit.AssetName = assetName;
        businessUnit.BusinessUnitName = BusinessUnitName ?? string.Empty;
        businessUnit.Organization = Organization ?? string.Empty;
        businessUnit.Description = Description ?? string.Empty;
        businessUnit.BusinessUnitHead = BusinessUnitHead ?? string.Empty;
        businessUnit.Email = Email ?? string.Empty;
        businessUnit.Phone = Phone ?? string.Empty;
        businessUnit.Status = Status;
        businessUnit.UpdatedBy = UpdatedBy;
        businessUnit.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record BusinessUnitResponse(
    string Id,
    string BusinessUnitCode,
    string AssetId,
    string AssetName,
    string BusinessUnitName,
    string Organization,
    string Description,
    string BusinessUnitHead,
    string Email,
    string Phone,
    string? Status,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static BusinessUnitResponse FromEntity(BusinessUnitEntity businessUnit)
    {
        return new BusinessUnitResponse(
            businessUnit.Id,
            businessUnit.BusinessUnitCode,
            businessUnit.AssetId,
            businessUnit.AssetName,
            businessUnit.BusinessUnitName,
            businessUnit.Organization,
            businessUnit.Description,
            businessUnit.BusinessUnitHead,
            businessUnit.Email,
            businessUnit.Phone,
            businessUnit.Status,
            businessUnit.CreatedBy,
            businessUnit.CreatedAt,
            businessUnit.UpdatedBy,
            businessUnit.UpdatedAt,
            businessUnit.ClientId,
            businessUnit.TenantId,
            businessUnit.IsDeleted);
    }
}
