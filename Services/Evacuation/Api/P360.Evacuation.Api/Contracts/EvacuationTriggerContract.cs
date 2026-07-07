
using EvacuationTriggerEntity = P360.Evacuation.Domain.Entities.EvacuationTrigger;

namespace P360.Evacuation.Api.Contracts;

public sealed record CreateEvacuationTriggerRequest(
    string? ReferenceId,
    string? TriggerField,
    string? IpAddress,
    string? ApplicationName,
    string? CreatedBy,
    string? ClientId)
{
    public EvacuationTriggerEntity ToEntity()
    {
        return new EvacuationTriggerEntity
        {
            ReferenceId = ReferenceId,
            TriggerField = TriggerField,
            IpAddress = IpAddress,
            ApplicationName = ApplicationName,

            CreatedBy = CreatedBy,
            ClientId = ClientId,

            CreatedAt = DateTime.UtcNow
        };
    }
}

public sealed record UpdateEvacuationTriggerRequest(
    string? TriggerField,
    string? IpAddress,
    string? ApplicationName)
{
    public void ApplyTo(EvacuationTriggerEntity evacuationTrigger)
    {
        evacuationTrigger.TriggerField = TriggerField;
        evacuationTrigger.IpAddress = IpAddress;
        evacuationTrigger.ApplicationName = ApplicationName;
    }
}

public sealed record EvacuationTriggerResponse(
    string Id,
    string ReferenceId,
    string TriggerField,
    string? IpAddress,
    string? ApplicationName,
    string CreatedBy,
    DateTime CreatedAt,
    string ClientId)
{
    public static EvacuationTriggerResponse FromEntity(
        EvacuationTriggerEntity evacuationTrigger)
    {
        return new EvacuationTriggerResponse(
            evacuationTrigger.Id ?? string.Empty,
            evacuationTrigger.ReferenceId ?? string.Empty,
            evacuationTrigger.TriggerField ?? string.Empty,

            evacuationTrigger.IpAddress,
            evacuationTrigger.ApplicationName,

            evacuationTrigger.CreatedBy ?? string.Empty,
            evacuationTrigger.CreatedAt,

            evacuationTrigger.ClientId ?? string.Empty
        );
    }
}
