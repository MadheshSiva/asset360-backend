using CostCenterEntity = A360.MasterManagement.Domain.Entities.CostCenter;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateCostCenterRequest(
    string? CostCenterName,
    string? CostCenterCode,
    string? Description,
    string? Department,
    string? ParentCostCenter,
    string? Manager,
    decimal BudgetAmount,
    string? Status,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public CostCenterEntity ToEntity(string costCenterId)
    {
        return new CostCenterEntity
        {
            CostCenterId = costCenterId,
            CostCenterName = CostCenterName ?? string.Empty,
            CostCenterCode = CostCenterCode ?? string.Empty,
            Description = Description ?? string.Empty,
            Department = Department ?? string.Empty,
            ParentCostCenter = ParentCostCenter,
            Manager = Manager ?? string.Empty,
            BudgetAmount = BudgetAmount,
            Status = Status,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateCostCenterRequest(
    string? CostCenterName,
    string? CostCenterCode,
    string? Description,
    string? Department,
    string? ParentCostCenter,
    string? Manager,
    decimal BudgetAmount,
    string? Status,
    string? UpdatedBy)
{
    public void ApplyTo(CostCenterEntity costCenter)
    {
        costCenter.CostCenterName = CostCenterName ?? string.Empty;
        costCenter.CostCenterCode = CostCenterCode ?? string.Empty;
        costCenter.Description = Description ?? string.Empty;
        costCenter.Department = Department ?? string.Empty;
        costCenter.ParentCostCenter = ParentCostCenter;
        costCenter.Manager = Manager ?? string.Empty;
        costCenter.BudgetAmount = BudgetAmount;
        costCenter.Status = Status;
        costCenter.UpdatedBy = UpdatedBy;
        costCenter.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record CostCenterResponse(
    string Id,
    string CostCenterId,
    string CostCenterName,
    string CostCenterCode,
    string Description,
    string Department,
    string? ParentCostCenter,
    string Manager,
    decimal BudgetAmount,
    string? Status,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static CostCenterResponse FromEntity(CostCenterEntity costCenter)
    {
        return new CostCenterResponse(
            costCenter.Id,
            costCenter.CostCenterId,
            costCenter.CostCenterName,
            costCenter.CostCenterCode,
            costCenter.Description,
            costCenter.Department,
            costCenter.ParentCostCenter,
            costCenter.Manager,
            costCenter.BudgetAmount,
            costCenter.Status,
            costCenter.CreatedBy,
            costCenter.CreatedAt,
            costCenter.UpdatedBy,
            costCenter.UpdatedAt,
            costCenter.ClientId,
            costCenter.TenantId,
            costCenter.IsDeleted);
    }
}
