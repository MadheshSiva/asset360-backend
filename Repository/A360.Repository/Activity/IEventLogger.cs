namespace A360.Repository.Activity;

public interface IEventLogger
{
    Task LogAsync(
        string entityType,
        string entityId,
        string entityName,
        string action,
        CancellationToken cancellationToken = default);
}
