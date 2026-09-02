using ApprovalEntity = A360.VisitorManagement.Domain.Entities.VisitorApproval;

namespace A360.VisitorManagement.Api.Contracts;

public sealed record CreateVisitorApprovalRequest(
    string? CreatedBy,
    string? Precedence,
    string? PermitType,
    List<string>? EmployeeEmailIds,
    string? ClientId,
    string? TenantId)
{
    public ApprovalEntity ToEntity()
    {
        return new ApprovalEntity
        {
            CreatedBy = CreatedBy,
            Precedence = Precedence,
            PermitType = PermitType,
            EmployeeEmailIds = EmployeeEmailIds ?? [],
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateVisitorApprovalRequest(
    string? Precedence,
    string? PermitType,
    List<string>? EmployeeEmailIds,
    string? UpdatedBy,
    string? Status)
{
    public void ApplyTo(ApprovalEntity approval)
    {
        approval.Precedence = Precedence;
        approval.PermitType = PermitType;
        approval.EmployeeEmailIds = EmployeeEmailIds ?? [];
        approval.UpdatedBy = UpdatedBy;
        approval.UpdatedAt = DateTime.UtcNow;
        if (!string.IsNullOrWhiteSpace(Status))
        {
            approval.Status = Status;
        }
    }
}

public sealed record VisitorApprovalResponse(
    string Id,
    string CreatedBy,
    DateTime? CreatedAt,
    string Precedence,
    string PermitType,
    List<string> EmployeeEmailIds,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? TenantId,
    bool IsDeleted,
    string? Status)
{
    public static VisitorApprovalResponse FromEntity(
        ApprovalEntity approval)
    {
        return new VisitorApprovalResponse(
            approval.Id ?? string.Empty,
            approval.CreatedBy ?? string.Empty,
            approval.CreatedAt,
            approval.Precedence ?? string.Empty,
            approval.PermitType ?? string.Empty,
            approval.EmployeeEmailIds,
            approval.UpdatedBy,
            approval.UpdatedAt,
            approval.TenantId,
            approval.IsDeleted,
            approval.Status);
    }
}
