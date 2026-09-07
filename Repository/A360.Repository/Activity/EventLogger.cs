using A360.Domain.Entities;
using A360.Repository.Repositories;

namespace A360.Repository.Activity;

public sealed class EventLogger : IEventLogger
{
    private readonly IEventLogRepository _repository;
    private readonly string _serviceName;

    public EventLogger(IEventLogRepository repository, string serviceName)
    {
        _repository = repository;
        _serviceName = serviceName;
    }

    public async Task LogAsync(
        string entityType,
        string entityId,
        string entityName,
        string action,
        CancellationToken cancellationToken = default)
    {
        await _repository.CreateAsync(new EventLog
        {
            ServiceName = _serviceName,
            EntityType = entityType,
            EntityId = entityId,
            EntityName = entityName,
            Action = action,
            CreatedAt = DateTime.UtcNow
        }, cancellationToken);
    }
}
