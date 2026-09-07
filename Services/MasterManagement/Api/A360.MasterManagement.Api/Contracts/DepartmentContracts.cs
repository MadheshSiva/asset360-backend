using DepartmentEntity = A360.MasterManagement.Domain.Entities.Department;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateDepartmentRequest(
    string? AssetId,
    string? DepartmentName,
    string? BusinessUnit,
    string? DepartmentHead,
    string? Description,
    string? Status,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public DepartmentEntity ToEntity(string departmentCode, string assetName)
    {
        return new DepartmentEntity
        {
            DepartmentCode = departmentCode,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            DepartmentName = DepartmentName ?? string.Empty,
            BusinessUnit = BusinessUnit ?? string.Empty,
            DepartmentHead = DepartmentHead ?? string.Empty,
            Description = Description ?? string.Empty,
            Status = Status,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateDepartmentRequest(
    string? AssetId,
    string? DepartmentName,
    string? BusinessUnit,
    string? DepartmentHead,
    string? Description,
    string? Status,
    string? UpdatedBy)
{
    public void ApplyTo(DepartmentEntity department, string assetName)
    {
        department.AssetId = AssetId ?? string.Empty;
        department.AssetName = assetName;
        department.DepartmentName = DepartmentName ?? string.Empty;
        department.BusinessUnit = BusinessUnit ?? string.Empty;
        department.DepartmentHead = DepartmentHead ?? string.Empty;
        department.Description = Description ?? string.Empty;
        department.Status = Status;
        department.UpdatedBy = UpdatedBy;
        department.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record DepartmentResponse(
    string Id,
    string DepartmentCode,
    string AssetId,
    string AssetName,
    string DepartmentName,
    string BusinessUnit,
    string DepartmentHead,
    string Description,
    string? Status,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static DepartmentResponse FromEntity(DepartmentEntity department)
    {
        return new DepartmentResponse(
            department.Id,
            department.DepartmentCode,
            department.AssetId,
            department.AssetName,
            department.DepartmentName,
            department.BusinessUnit,
            department.DepartmentHead,
            department.Description,
            department.Status,
            department.CreatedBy,
            department.CreatedAt,
            department.UpdatedBy,
            department.UpdatedAt,
            department.ClientId,
            department.TenantId,
            department.IsDeleted);
    }
}
