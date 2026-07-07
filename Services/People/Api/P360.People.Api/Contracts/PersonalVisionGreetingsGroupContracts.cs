using GroupsEntity = P360.People.Domain.Entities.PersonalVisionGreetingsGroups;
using P360.People.Domain.Entities;

namespace P360.People.Api.Contracts;

public sealed record CreateGreetingsGroupMemberRequest(
string? MemberId,
string? MemberName);

public sealed record UpdateGreetingsGroupMemberRequest(
string? MemberId,
string? MemberName);

public sealed record GreetingsGroupMemberResponse(
string MemberId,
string MemberName);

public sealed record CreateGreetingsTimeScheduleRequest1(
DateTime FromDate,
DateTime ToDate,
string? FromTime,
string? ToTime);

public sealed record UpdateGreetingsTimeScheduleRequest1(
DateTime FromDate,
DateTime ToDate,
string? FromTime,
string? ToTime);

public sealed record GreetingsTimeScheduleResponse1(
DateTime FromDate,
DateTime ToDate,
string FromTime,
string ToTime);

public sealed record CreatePersonalVisionGreetingsGroupsRequest(
List<CreateGreetingsGroupMemberRequest>? Members,
string? GroupType,
string? GroupName,
string? GreetingsType,
string? GreetingsDescription,
bool Status,
List<CreateGreetingsTimeScheduleRequest>? GreetingsTimeSchedules,
string? CreatedBy)
{
public GroupsEntity ToEntity()
{
return new GroupsEntity
{
Members = Members?.Select(x => new GreetingsGroupMember
{
MemberId = x.MemberId ?? string.Empty,
MemberName = x.MemberName ?? string.Empty
}).ToList(),


        GroupType = GroupType,
        GroupName = GroupName,
        GreetingsType = GreetingsType,
        GreetingsDescription = GreetingsDescription,
        Status = Status,

        GreetingsTimeSchedules =
            GreetingsTimeSchedules?.Select(x => new GreetingsTimeSchedule1
            {
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                FromTime = x.FromTime ?? string.Empty,
                ToTime = x.ToTime ?? string.Empty
            }).ToList(),

        CreatedBy = CreatedBy,
        CreatedAt = DateTime.UtcNow
    };
}


}

public sealed record UpdatePersonalVisionGreetingsGroupsRequest(
List<UpdateGreetingsGroupMemberRequest>? Members,
string? GroupType,
string? GroupName,
string? GreetingsType,
string? GreetingsDescription,
bool Status,
List<UpdateGreetingsTimeScheduleRequest>? GreetingsTimeSchedules)
{
public void ApplyTo(GroupsEntity entity)
{
entity.Members = Members?.Select(x => new GreetingsGroupMember
{
MemberId = x.MemberId ?? string.Empty,
MemberName = x.MemberName ?? string.Empty
}).ToList();

    entity.GroupType = GroupType;
    entity.GroupName = GroupName;
    entity.GreetingsType = GreetingsType;
    entity.GreetingsDescription = GreetingsDescription;
    entity.Status = Status;

    entity.GreetingsTimeSchedules =
        GreetingsTimeSchedules?.Select(x => new GreetingsTimeSchedule1
        {
            FromDate = x.FromDate,
            ToDate = x.ToDate,
            FromTime = x.FromTime ?? string.Empty,
            ToTime = x.ToTime ?? string.Empty
        }).ToList();

    entity.ModifiedAt = DateTime.UtcNow;
}


}

public sealed record PersonalVisionGreetingsGroupsResponse(
string Id,
List<GreetingsGroupMemberResponse> Members,
string GroupType,
string GroupName,
string GreetingsType,
string GreetingsDescription,
bool Status,
List<GreetingsTimeScheduleResponse> GreetingsTimeSchedules,
string CreatedBy,
DateTime CreatedAt)
{
public static PersonalVisionGreetingsGroupsResponse FromEntity(
GroupsEntity entity)
{
return new PersonalVisionGreetingsGroupsResponse(
entity.Id ?? string.Empty,


        entity.Members?
            .Select(x => new GreetingsGroupMemberResponse(
                x.MemberId ?? string.Empty,
                x.MemberName ?? string.Empty))
            .ToList()
            ?? [],

        entity.GroupType ?? string.Empty,
        entity.GroupName ?? string.Empty,
        entity.GreetingsType ?? string.Empty,
        entity.GreetingsDescription ?? string.Empty,
        entity.Status,

        entity.GreetingsTimeSchedules?
            .Select(x => new GreetingsTimeScheduleResponse(
                x.FromDate,
                x.ToDate,
                x.FromTime ?? string.Empty,
                x.ToTime ?? string.Empty))
            .ToList()
            ?? [],

        entity.CreatedBy ?? string.Empty,
        entity.CreatedAt);
}


}
