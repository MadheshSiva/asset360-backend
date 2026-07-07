using ManualAttendanceEntity = A360.People.Domain.Entities.PersonalVisionManualAttendance;

namespace A360.People.Api.Contracts;

public sealed record CreatePersonalVisionManualAttendanceRequest(
    string EmployeeId,
    string? EmployeeName,
    string? Reason,
    DateTime FromDate,
    string? FromTime,
    string? AttendanceStatus)
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
            CreatedAt = DateTime.UtcNow
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
    string? Action)
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

        attendance.ModifiedAt = DateTime.UtcNow;
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
    DateTime CreatedAt,
    string? ModifiedBy,
    DateTime? ModifiedAt,
    string? ApprovedBy,
    DateTime? ApprovedOn,
    string? ApprovedRemarks,
    string? Action)
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
            attendance.ModifiedBy,
            attendance.ModifiedAt,
            attendance.ApprovedBy,
            attendance.ApprovedOn,
            attendance.ApprovedRemarks,
            attendance.Action);
    }
}