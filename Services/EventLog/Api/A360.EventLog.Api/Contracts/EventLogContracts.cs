using EventLogEntity = A360.Domain.Entities.EventLog;

namespace A360.EventLog.Api.Contracts;

public sealed record EventLogResponse(
    string Id,
    string ServiceName,
    string EntityType,
    string EntityId,
    string EntityName,
    string Action,
    DateTime? CreatedAt)
{
    public static EventLogResponse FromEntity(EventLogEntity eventLog)
    {
        return new EventLogResponse(
            eventLog.Id,
            eventLog.ServiceName,
            eventLog.EntityType,
            eventLog.EntityId,
            eventLog.EntityName,
            eventLog.Action,
            eventLog.CreatedAt);
    }
}

public sealed record EventLogListResponse(
    IReadOnlyCollection<EventLogResponse> Items,
    long TotalCount,
    int Page,
    int PageSize);
