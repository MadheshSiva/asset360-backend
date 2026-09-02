using ManualAttendanceEntity = A360.People.Domain.Entities.PersonalVisionManualAttendance;

namespace A360.People.Api.Contracts;

public sealed record CreatePersonalVisionManualAttendanceRequest(
    string EmployeeId,
    string? EmployeeName,
    string? Reason,
    DateTime FromDate,
    string? FromTime,
    string? AttendanceStatus,
    string? ClientId,
    string? TenantId)
{
    public ManualAttendanceEntity ToEntity()
    {
        return new ManualAttendanceEntity
        {
            EmployeeId = EmployeeId,
            EmployeeName = EmployeeName,
            Reason = Reason,
            FromDate = FromDate,
            FromTime = FromTime,
            AttendanceStatus = AttendanceStatus,
            ApproveStatus = "Pending",
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdatePersonalVisionManualAttendanceRequest(
    string EmployeeId,
    string? EmployeeName,
    string? Reason,
    DateTime FromDate,
    string? FromTime,
    string? AttendanceStatus,
    string? ApproveStatus,
    string? ApprovedBy,
    string? ApprovedRemarks,
    string? Action,
    string? UpdatedBy,
    string? Status)
{
    public void ApplyTo(ManualAttendanceEntity attendance)
    {
        attendance.EmployeeId = EmployeeId;
        attendance.EmployeeName = EmployeeName;
        attendance.Reason = Reason;
        attendance.FromDate = FromDate;
        attendance.FromTime = FromTime;
        attendance.AttendanceStatus = AttendanceStatus;
        attendance.ApproveStatus = ApproveStatus;
        attendance.ApprovedBy = ApprovedBy;
        attendance.ApprovedRemarks = ApprovedRemarks;
        attendance.Action = Action;

        if (!string.IsNullOrWhiteSpace(ApprovedBy))
        {
            attendance.ApprovedOn = DateTime.UtcNow;
        }

        attendance.UpdatedBy = UpdatedBy;
        attendance.UpdatedAt = DateTime.UtcNow;
        if (!string.IsNullOrWhiteSpace(Status))
        {
            attendance.Status = Status;
        }
    }
}

public sealed record PersonalVisionManualAttendanceResponse(
    string Id,
    string EmployeeId,
    string? EmployeeName,
    string? Reason,
    DateTime FromDate,
    string? FromTime,
    string? AttendanceStatus,
    string? ApproveStatus,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ApprovedBy,
    DateTime? ApprovedOn,
    string? ApprovedRemarks,
    string? Action,
    string? ClientId,
    string? TenantId,
    string? Status,
    bool IsDeleted)
{
    public static PersonalVisionManualAttendanceResponse FromEntity(
        ManualAttendanceEntity attendance)
    {
        return new PersonalVisionManualAttendanceResponse(
            attendance.Id ?? string.Empty,
            attendance.EmployeeId,
            attendance.EmployeeName,
            attendance.Reason,
            attendance.FromDate,
            attendance.FromTime,
            attendance.AttendanceStatus,
            attendance.ApproveStatus,
            attendance.CreatedBy,
            attendance.CreatedAt,
            attendance.UpdatedBy,
            attendance.UpdatedAt,
            attendance.ApprovedBy,
            attendance.ApprovedOn,
            attendance.ApprovedRemarks,
            attendance.Action,
            attendance.ClientId,
            attendance.TenantId,
            attendance.Status,
            attendance.IsDeleted);
    }
}