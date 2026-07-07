using WorkScheduleEntity = P360.People.Domain.Entities.PersonalWorkSchedule;
using P360.People.Domain.Entities;

namespace P360.People.Api.Contracts;

public sealed record CreatePersonalWorkScheduleRequest(
    string? WorkScheduleName,
    string? Description,
    string? Location,
    string? GroupName,
    string? GroupId,
    bool Status,
    List<WorkScheduleItem>? WorkSchedules,
    string? CreatedBy,
    string? ScheduleType,
    List<ScheduleMember>? Member)
{
    public WorkScheduleEntity ToEntity()
    {
        return new WorkScheduleEntity
        {
            WorkScheduleName = WorkScheduleName,
            Description = Description,
            Location = Location,
            GroupName = GroupName,
            GroupId = GroupId,
            Status = Status,
            WorkSchedules = WorkSchedules ?? [],
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ScheduleType = ScheduleType,
            Member = Member ?? []
        };
    }
}

public sealed record UpdatePersonalWorkScheduleRequest(
    string? WorkScheduleName,
    string? Description,
    string? Location,
    string? GroupName,
    string? GroupId,
    bool Status,
    List<WorkScheduleItem>? WorkSchedules,
    string? ScheduleType,
    List<ScheduleMember>? Member)
{
    public void ApplyTo(WorkScheduleEntity workSchedule)
    {
        workSchedule.WorkScheduleName = WorkScheduleName;
        workSchedule.Description = Description;
        workSchedule.Location = Location;
        workSchedule.GroupName = GroupName;
        workSchedule.GroupId = GroupId;
        workSchedule.Status = Status;
        workSchedule.WorkSchedules = WorkSchedules ?? [];
        workSchedule.ScheduleType = ScheduleType;
        workSchedule.Member = Member ?? [];
    }
}

public sealed record PersonalWorkScheduleResponse(
    string Id,
    string WorkScheduleName,
    string Description,
    string Location,
    string GroupName,
    string GroupId,
    bool Status,
    List<WorkScheduleItem> WorkSchedules,
    string CreatedBy,
    DateTime CreatedAt,
    string ScheduleType,
    List<ScheduleMember> Member)
{
    public static PersonalWorkScheduleResponse FromEntity(
        WorkScheduleEntity workSchedule)
    {
        return new PersonalWorkScheduleResponse(
            workSchedule.Id ?? string.Empty,
            workSchedule.WorkScheduleName ?? string.Empty,
            workSchedule.Description ?? string.Empty,
            workSchedule.Location ?? string.Empty,
            workSchedule.GroupName ?? string.Empty,
            workSchedule.GroupId ?? string.Empty,
            workSchedule.Status,
            workSchedule.WorkSchedules ?? [],
            workSchedule.CreatedBy ?? string.Empty,
            workSchedule.CreatedAt,
            workSchedule.ScheduleType ?? string.Empty,
            workSchedule.Member ?? []);
    }
}