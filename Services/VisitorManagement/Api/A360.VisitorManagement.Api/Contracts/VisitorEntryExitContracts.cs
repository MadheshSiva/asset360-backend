using EntryExitEntity = A360.VisitorManagement.Domain.Entities.VisitorEntryExit;

namespace A360.VisitorManagement.Api.Contracts;

public sealed record CreateVisitorEntryExitRequest(
    string? Name,
    string? Type,
    string? Description,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public EntryExitEntity ToEntity()
    {
        return new EntryExitEntity
        {
            Name = Name,
            Type = Type,
            Description = Description,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateVisitorEntryExitRequest(
    string? Name,
    string? Type,
    string? Description,
    string? UpdatedBy,
    string? Status)
{
    public void ApplyTo(EntryExitEntity entryExit)
    {
        entryExit.Name = Name;
        entryExit.Type = Type;
        entryExit.Description = Description;
        entryExit.UpdatedBy = UpdatedBy;
        entryExit.UpdatedAt = DateTime.UtcNow;
        if (!string.IsNullOrWhiteSpace(Status))
        {
            entryExit.Status = Status;
        }
    }
}

public sealed record VisitorEntryExitResponse(
    string Id,
    string Name,
    string Type,
    string Description,
    string CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? TenantId,
    bool IsDeleted,
    string? Status)
{
    public static VisitorEntryExitResponse FromEntity(
        EntryExitEntity entryExit)
    {
        return new VisitorEntryExitResponse(
            entryExit.Id ?? string.Empty,
            entryExit.Name ?? string.Empty,
            entryExit.Type ?? string.Empty,
            entryExit.Description ?? string.Empty,
            entryExit.CreatedBy ?? string.Empty,
            entryExit.CreatedAt,
            entryExit.UpdatedBy,
            entryExit.UpdatedAt,
            entryExit.TenantId,
            entryExit.IsDeleted,
            entryExit.Status);
    }
}
