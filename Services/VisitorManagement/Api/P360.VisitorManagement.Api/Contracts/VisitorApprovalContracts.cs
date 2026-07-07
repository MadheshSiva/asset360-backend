using ApprovalEntity = P360.VisitorManagement.Domain.Entities.VisitorApproval;

namespace P360.VisitorManagement.Api.Contracts;

public sealed record CreateVisitorApprovalRequest(
    string? CreatedBy,
    string? Precedence,
    string? PermitType,
    List<string>? EmployeeEmailIds)
{
    public ApprovalEntity ToEntity()
    {
        return new ApprovalEntity
        {
            CreatedBy = CreatedBy,
            Precedence = Precedence,
            PermitType = PermitType,
            EmployeeEmailIds = EmployeeEmailIds ?? [],
            CreatedAt = DateTime.UtcNow
        };
    }
}

public sealed record UpdateVisitorApprovalRequest(
    string? Precedence,
    string? PermitType,
    List<string>? EmployeeEmailIds)
{
    public void ApplyTo(ApprovalEntity approval)
    {
        approval.Precedence = Precedence;
        approval.PermitType = PermitType;
        approval.EmployeeEmailIds = EmployeeEmailIds ?? [];
    }
}

public sealed record VisitorApprovalResponse(
    string Id,
    string CreatedBy,
    DateTime CreatedAt,
    string Precedence,
    string PermitType,
    List<string> EmployeeEmailIds)
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
            approval.EmployeeEmailIds);
    }
}
