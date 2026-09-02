using ReconcilePassEntity = A360.VisitorManagement.Domain.Entities.VisitorReconcilePass;

namespace A360.VisitorManagement.Api.Contracts;

public sealed record CreateVisitorReconcilePassRequest(
    string? NumberOfVisitors,
    string? NumberOfPeopleExited,
    string? VisitorPhysicallyPresent,
    string? VerifiedSecurityEmpNo,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public ReconcilePassEntity ToEntity()
    {
        return new ReconcilePassEntity
        {
            NumberOfVisitors = NumberOfVisitors,
            NumberOfPeopleExited = NumberOfPeopleExited,
            VisitorPhysicallyPresent = VisitorPhysicallyPresent,
            VerifiedSecurityEmpNo = VerifiedSecurityEmpNo,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateVisitorReconcilePassRequest(
    string? NumberOfVisitors,
    string? NumberOfPeopleExited,
    string? VisitorPhysicallyPresent,
    string? VerifiedSecurityEmpNo,
    string? UpdatedBy,
    string? Status)
{
    public void ApplyTo(ReconcilePassEntity reconcilePass)
    {
        reconcilePass.NumberOfVisitors = NumberOfVisitors;
        reconcilePass.NumberOfPeopleExited = NumberOfPeopleExited;
        reconcilePass.VisitorPhysicallyPresent = VisitorPhysicallyPresent;
        reconcilePass.VerifiedSecurityEmpNo = VerifiedSecurityEmpNo;
        reconcilePass.UpdatedBy = UpdatedBy;
        reconcilePass.UpdatedAt = DateTime.UtcNow;
        if (!string.IsNullOrWhiteSpace(Status))
        {
            reconcilePass.Status = Status;
        }
    }
}

public sealed record VisitorReconcilePassResponse(
    string Id,
    string NumberOfVisitors,
    string NumberOfPeopleExited,
    string VisitorPhysicallyPresent,
    string VerifiedSecurityEmpNo,
    string CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? TenantId,
    bool IsDeleted,
    string? Status)
{
    public static VisitorReconcilePassResponse FromEntity(
        ReconcilePassEntity reconcilePass)
    {
        return new VisitorReconcilePassResponse(
            reconcilePass.Id ?? string.Empty,
            reconcilePass.NumberOfVisitors ?? string.Empty,
            reconcilePass.NumberOfPeopleExited ?? string.Empty,
            reconcilePass.VisitorPhysicallyPresent ?? string.Empty,
            reconcilePass.VerifiedSecurityEmpNo ?? string.Empty,
            reconcilePass.CreatedBy ?? string.Empty,
            reconcilePass.CreatedAt,
            reconcilePass.UpdatedBy,
            reconcilePass.UpdatedAt,
            reconcilePass.TenantId,
            reconcilePass.IsDeleted,
            reconcilePass.Status);
    }
}
