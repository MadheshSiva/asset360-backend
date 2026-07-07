using GreetingsEntity = P360.People.Domain.Entities.PersonalVisionGreetingsIndividual;
using P360.People.Domain.Entities;


namespace P360.People.Api.Contracts;

public sealed record CreateGreetingsTimeScheduleRequest(
    DateTime FromDate,
    DateTime ToDate,
    string? FromTime,
    string? ToTime);

public sealed record UpdateGreetingsTimeScheduleRequest(
    DateTime FromDate,
    DateTime ToDate,
    string? FromTime,
    string? ToTime);

public sealed record GreetingsTimeScheduleResponse(
    DateTime FromDate,
    DateTime ToDate,
    string FromTime,
    string ToTime);

public sealed record CreateGreetingsIndividualMemberRequest(
    string? MemberId,
    string? MemberName);

public sealed record UpdateGreetingsIndividualMemberRequest(
    string? MemberId,
    string? MemberName);

public sealed record GreetingsIndividualMemberResponse(
    string MemberId,
    string MemberName);

public sealed record CreatePersonalVisionGreetingsIndividualRequest(
    List<CreateGreetingsIndividualMemberRequest>? MemberList,
    string? MemberType,
    string? GreetingsType,
    string? GreetingsDescription,
    bool Status,
    List<CreateGreetingsTimeScheduleRequest>? GreetingsTimeSchedules,
    string? CreatedBy)
{
    public GreetingsEntity ToEntity()
    {
        return new GreetingsEntity
        {
            MemberList =
                MemberList?.Select(x => new GreetingsIndividualMember
                {
                    MemberId = x.MemberId ?? string.Empty,
                    MemberName = x.MemberName ?? string.Empty
                }).ToList() ?? new(),
            MemberType = MemberType,
            GreetingsType = GreetingsType,
            GreetingsDescription = GreetingsDescription,
            Status = Status,
            GreetingsTimeSchedules =
                GreetingsTimeSchedules?.Select(x => new GreetingsTimeSchedule
                {
                    FromDate = x.FromDate,
                    ToDate = x.ToDate,
                    FromTime = x.FromTime,
                    ToTime = x.ToTime
                }).ToList(),
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public sealed record UpdatePersonalVisionGreetingsIndividualRequest(
    List<UpdateGreetingsIndividualMemberRequest>? MemberList,
    string? MemberType,
    string? GreetingsType,
    string? GreetingsDescription,
    bool Status,
    List<UpdateGreetingsTimeScheduleRequest>? GreetingsTimeSchedules)
{
    public void ApplyTo(
        GreetingsEntity entity)
    {
        entity.MemberList =
            MemberList?.Select(x => new GreetingsIndividualMember
            {
                MemberId = x.MemberId ?? string.Empty,
                MemberName = x.MemberName ?? string.Empty
            }).ToList() ?? new();
        entity.MemberType = MemberType;
        entity.GreetingsType = GreetingsType;
        entity.GreetingsDescription = GreetingsDescription;
        entity.Status = Status;

        entity.GreetingsTimeSchedules =
            GreetingsTimeSchedules?.Select(x => new GreetingsTimeSchedule
            {
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                FromTime = x.FromTime,
                ToTime = x.ToTime
            }).ToList();

        entity.ModifiedAt = DateTime.UtcNow;
    }
}

public sealed record PersonalVisionGreetingsIndividualResponse(
    string Id,
    List<GreetingsIndividualMemberResponse> MemberList,
    string MemberType,
    string GreetingsType,
    string GreetingsDescription,
    bool Status,
    List<GreetingsTimeScheduleResponse> GreetingsTimeSchedules,
    string CreatedBy,
    DateTime CreatedAt)
{
    public static PersonalVisionGreetingsIndividualResponse FromEntity(
        GreetingsEntity entity)
    {
        return new PersonalVisionGreetingsIndividualResponse(
            entity.Id ?? string.Empty,
            entity.MemberList?
                .Select(x => new GreetingsIndividualMemberResponse(
                    x.MemberId ?? string.Empty,
                    x.MemberName ?? string.Empty))
                .ToList()
                ?? [],
            entity.MemberType ?? string.Empty,
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