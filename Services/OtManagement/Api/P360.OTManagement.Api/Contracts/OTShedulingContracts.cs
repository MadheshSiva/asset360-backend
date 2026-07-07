using OTSchedulingEntity =
    P360.OTManagement.Domain.Entities.OTScheduling;

namespace P360.OTManagement.Api.Contracts;

public sealed record CreateOTSchedulingRequest(
    string? ScheduleId,
    string? ResourceId,
    string? Surgeon,
    DateTime StartTime,
    DateTime EndTime,
    string? SurgeryType,
    string? Priority,
    bool Status,
    string? CreatedBy)
{
    public OTSchedulingEntity ToEntity()
    {
        return new OTSchedulingEntity
        {
            ScheduleId = ScheduleId,
            ResourceId = ResourceId,
            Surgeon = Surgeon,
            StartTime = StartTime,
            EndTime = EndTime,
            SurgeryType = SurgeryType,
            Priority = Priority,
            Status = Status,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public sealed record UpdateOTSchedulingRequest(
    string? ResourceId,
    string? Surgeon,
    DateTime StartTime,
    DateTime EndTime,
    string? SurgeryType,
    string? Priority,
    bool Status)
{
    public void ApplyTo(
        OTSchedulingEntity otScheduling)
    {
        otScheduling.ResourceId = ResourceId;
        otScheduling.Surgeon = Surgeon;
        otScheduling.StartTime = StartTime;
        otScheduling.EndTime = EndTime;
        otScheduling.SurgeryType = SurgeryType;
        otScheduling.Priority = Priority;
        otScheduling.Status = Status;
    }
}

public sealed record OTSchedulingResponse(
    string Id,
    string ScheduleId,
    string ResourceId,
    string Surgeon,
    DateTime StartTime,
    DateTime EndTime,
    string SurgeryType,
    string Priority,
    bool Status,
    string CreatedBy,
    DateTime CreatedAt)
{
    public static OTSchedulingResponse FromEntity(
        OTSchedulingEntity otScheduling)
    {
        return new OTSchedulingResponse(
            otScheduling.Id ?? string.Empty,
            otScheduling.ScheduleId ?? string.Empty,
            otScheduling.ResourceId ?? string.Empty,
            otScheduling.Surgeon ?? string.Empty,
            otScheduling.StartTime,
            otScheduling.EndTime,
            otScheduling.SurgeryType ?? string.Empty,
            otScheduling.Priority ?? string.Empty,
            otScheduling.Status,
            otScheduling.CreatedBy ?? string.Empty,
            otScheduling.CreatedAt);
    }
}