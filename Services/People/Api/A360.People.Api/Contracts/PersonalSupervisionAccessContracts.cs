using AccessEntity = A360.People.Domain.Entities.PersonalVisionAccess;
using A360.People.Domain.Entities;

namespace A360.People.Api.Contracts;

public sealed record CreatePersonalVisionAccessRequest(
    string? GroupName,
    string? GroupType,
    List<Member>? Member,
    List<Reader>? Reader,
    bool Status,
    List<AccessTimeSchedule>? AccessTimeSchedule,
    string? CreatedBy,
    string? Action,
    string? ClientId,
    string? TenantId)
{
    public AccessEntity ToEntity()
    {
        return new AccessEntity
        {
            GroupName = GroupName,
            GroupType = GroupType,
            Member = Member ?? [],
            Reader = Reader ?? [],
            Status = Status,
            AccessTimeSchedule = AccessTimeSchedule ?? [],
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            Action = Action,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdatePersonalVisionAccessRequest(
    string? GroupName,
    string? GroupType,
    List<Member>? Member,
    List<Reader>? Reader,
    bool Status,
    List<AccessTimeSchedule>? AccessTimeSchedule,
    string? UpdatedBy,
    string? Action)
{
    public void ApplyTo(AccessEntity access)
    {
        access.GroupName = GroupName;
        access.GroupType = GroupType;
        access.Member = Member ?? [];
        access.Reader = Reader ?? [];
        access.Status = Status;
        access.AccessTimeSchedule = AccessTimeSchedule ?? [];
        access.UpdatedBy = UpdatedBy;
        access.UpdatedAt = DateTime.UtcNow;
        access.Action = Action;
    }
}

public sealed record PersonalVisionAccessResponse(
    string Id,
    string GroupName,
    string GroupType,
    List<Member> Member,
    List<Reader> Reader,
    bool Status,
    List<AccessTimeSchedule> AccessTimeSchedule,
    string CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? Action,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static PersonalVisionAccessResponse FromEntity(
        AccessEntity access)
    {
        return new PersonalVisionAccessResponse(
            access.Id ?? string.Empty,
            access.GroupName ?? string.Empty,
            access.GroupType ?? string.Empty,
            access.Member ?? [],
            access.Reader ?? [],
            access.Status,
            access.AccessTimeSchedule ?? [],
            access.CreatedBy ?? string.Empty,
            access.CreatedAt,
            access.UpdatedBy,
            access.UpdatedAt,
            access.Action,
            access.ClientId,
            access.TenantId,
            access.IsDeleted
        );
    }
}