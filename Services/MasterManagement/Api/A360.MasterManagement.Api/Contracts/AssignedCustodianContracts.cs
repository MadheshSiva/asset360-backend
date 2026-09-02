using AssignedCustodianEntity = A360.MasterManagement.Domain.Entities.AssignedCustodian;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateAssignedCustodianRequest(
    string? DepartmentOrCustodian,
    string? Name,
    string? CustodianId,
    string? AssetId,
    string? Description,
    string? Status,
    string? Role,
    string? DepartmentCode,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssignedCustodianEntity ToEntity(string assignedCustodianId, string assetName)
    {
        return new AssignedCustodianEntity
        {
            AssignedCustodianId = assignedCustodianId,
            DepartmentOrCustodian = DepartmentOrCustodian ?? string.Empty,
            Name = Name ?? string.Empty,
            CustodianId = CustodianId ?? string.Empty,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            Description = Description ?? string.Empty,
            Status = Status,
            Role = Role ?? string.Empty,
            DepartmentCode = DepartmentCode ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssignedCustodianRequest(
    string? DepartmentOrCustodian,
    string? Name,
    string? CustodianId,
    string? AssetId,
    string? Description,
    string? Status,
    string? Role,
    string? DepartmentCode,
    string? UpdatedBy)
{
    public void ApplyTo(AssignedCustodianEntity assignedCustodian, string assetName)
    {
        assignedCustodian.DepartmentOrCustodian = DepartmentOrCustodian ?? string.Empty;
        assignedCustodian.Name = Name ?? string.Empty;
        assignedCustodian.CustodianId = CustodianId ?? string.Empty;
        assignedCustodian.AssetId = AssetId ?? string.Empty;
        assignedCustodian.AssetName = assetName;
        assignedCustodian.Description = Description ?? string.Empty;
        assignedCustodian.Status = Status;
        assignedCustodian.Role = Role ?? string.Empty;
        assignedCustodian.DepartmentCode = DepartmentCode ?? string.Empty;
        assignedCustodian.UpdatedBy = UpdatedBy;
        assignedCustodian.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssignedCustodianResponse(
    string Id,
    string AssignedCustodianId,
    string DepartmentOrCustodian,
    string Name,
    string CustodianId,
    string AssetId,
    string AssetName,
    string Description,
    string? Status,
    string Role,
    string DepartmentCode,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssignedCustodianResponse FromEntity(AssignedCustodianEntity assignedCustodian)
    {
        return new AssignedCustodianResponse(
            assignedCustodian.Id,
            assignedCustodian.AssignedCustodianId,
            assignedCustodian.DepartmentOrCustodian,
            assignedCustodian.Name,
            assignedCustodian.CustodianId,
            assignedCustodian.AssetId,
            assignedCustodian.AssetName,
            assignedCustodian.Description,
            assignedCustodian.Status,
            assignedCustodian.Role,
            assignedCustodian.DepartmentCode,
            assignedCustodian.CreatedBy,
            assignedCustodian.CreatedAt,
            assignedCustodian.UpdatedBy,
            assignedCustodian.UpdatedAt,
            assignedCustodian.ClientId,
            assignedCustodian.TenantId,
            assignedCustodian.IsDeleted);
    }
}
