using SupplierEntity = A360.MasterManagement.Domain.Entities.Supplier;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateSupplierRequest(
    string? AssetId,
    string? SupplierName,
    string? ContactPerson,
    string? Email,
    string? Phone,
    string? Address,
    string? ContractReference,
    string? Status,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public SupplierEntity ToEntity(string supplierCode, string assetName)
    {
        return new SupplierEntity
        {
            SupplierCode = supplierCode,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            SupplierName = SupplierName ?? string.Empty,
            ContactPerson = ContactPerson ?? string.Empty,
            Email = Email ?? string.Empty,
            Phone = Phone ?? string.Empty,
            Address = Address ?? string.Empty,
            ContractReference = ContractReference ?? string.Empty,
            Status = Status,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateSupplierRequest(
    string? AssetId,
    string? SupplierName,
    string? ContactPerson,
    string? Email,
    string? Phone,
    string? Address,
    string? ContractReference,
    string? Status,
    string? UpdatedBy)
{
    public void ApplyTo(SupplierEntity supplier, string assetName)
    {
        supplier.AssetId = AssetId ?? string.Empty;
        supplier.AssetName = assetName;
        supplier.SupplierName = SupplierName ?? string.Empty;
        supplier.ContactPerson = ContactPerson ?? string.Empty;
        supplier.Email = Email ?? string.Empty;
        supplier.Phone = Phone ?? string.Empty;
        supplier.Address = Address ?? string.Empty;
        supplier.ContractReference = ContractReference ?? string.Empty;
        supplier.Status = Status;
        supplier.UpdatedBy = UpdatedBy;
        supplier.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record SupplierResponse(
    string Id,
    string SupplierCode,
    string AssetId,
    string AssetName,
    string SupplierName,
    string ContactPerson,
    string Email,
    string Phone,
    string Address,
    string ContractReference,
    string? Status,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static SupplierResponse FromEntity(SupplierEntity supplier)
    {
        return new SupplierResponse(
            supplier.Id,
            supplier.SupplierCode,
            supplier.AssetId,
            supplier.AssetName,
            supplier.SupplierName,
            supplier.ContactPerson,
            supplier.Email,
            supplier.Phone,
            supplier.Address,
            supplier.ContractReference,
            supplier.Status,
            supplier.CreatedBy,
            supplier.CreatedAt,
            supplier.UpdatedBy,
            supplier.UpdatedAt,
            supplier.ClientId,
            supplier.TenantId,
            supplier.IsDeleted);
    }
}
