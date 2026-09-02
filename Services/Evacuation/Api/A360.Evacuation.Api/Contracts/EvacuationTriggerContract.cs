
using EvacuationTriggerEntity = A360.Evacuation.Domain.Entities.EvacuationTrigger;

namespace A360.Evacuation.Api.Contracts;

public sealed record CreateEvacuationTriggerRequest(
    string? ReferenceId,
    string? TriggerField,
    string? IpAddress,
    string? ApplicationName,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
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
            TenantId = TenantId,

            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
    }
}

public sealed record UpdateEvacuationTriggerRequest(
    string? TriggerField,
    string? IpAddress,
    string? ApplicationName,
    string? UpdatedBy,
    string? Status)
{
    public void ApplyTo(EvacuationTriggerEntity evacuationTrigger)
    {
        evacuationTrigger.TriggerField = TriggerField;
        evacuationTrigger.IpAddress = IpAddress;
        evacuationTrigger.ApplicationName = ApplicationName;

        evacuationTrigger.UpdatedBy = UpdatedBy;
        evacuationTrigger.UpdatedAt = DateTime.UtcNow;
        if (!string.IsNullOrWhiteSpace(Status))
        {
            evacuationTrigger.Status = Status;
        }
    }
}

public sealed record EvacuationTriggerResponse(
    string Id,
    string ReferenceId,
    string TriggerField,
    string? IpAddress,
    string? ApplicationName,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? ClientId,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? TenantId,
    string? Status,
    bool IsDeleted)
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

            evacuationTrigger.CreatedBy,
            evacuationTrigger.CreatedAt,

            evacuationTrigger.ClientId,

            evacuationTrigger.UpdatedBy,
            evacuationTrigger.UpdatedAt,
            evacuationTrigger.TenantId,
            evacuationTrigger.Status,
            evacuationTrigger.IsDeleted
        );
    }
}
