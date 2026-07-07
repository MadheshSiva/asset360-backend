using ReconcilePassEntity = A360.VisitorManagement.Domain.Entities.VisitorReconcilePass;

namespace A360.VisitorManagement.Api.Contracts;

public sealed record CreateVisitorReconcilePassRequest(
    string? NumberOfVisitors,
    string? NumberOfPeopleExited,
    string? VisitorPhysicallyPresent,
    string? VerifiedSecurityEmpNo,
    string? CreatedBy)
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
            CreatedAt = DateTime.UtcNow
        };
    }
}

public sealed record UpdateVisitorReconcilePassRequest(
    string? NumberOfVisitors,
    string? NumberOfPeopleExited,
    string? VisitorPhysicallyPresent,
    string? VerifiedSecurityEmpNo)
{
    public void ApplyTo(ReconcilePassEntity reconcilePass)
    {
        reconcilePass.NumberOfVisitors = NumberOfVisitors;
        reconcilePass.NumberOfPeopleExited = NumberOfPeopleExited;
        reconcilePass.VisitorPhysicallyPresent = VisitorPhysicallyPresent;
        reconcilePass.VerifiedSecurityEmpNo = VerifiedSecurityEmpNo;
    }
}

public sealed record VisitorReconcilePassResponse(
    string Id,
    string NumberOfVisitors,
    string NumberOfPeopleExited,
    string VisitorPhysicallyPresent,
    string VerifiedSecurityEmpNo,
    string CreatedBy,
    DateTime CreatedAt)
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
            reconcilePass.CreatedAt);
    }
}
