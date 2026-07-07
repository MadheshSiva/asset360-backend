using EntryExitEntity = P360.VisitorManagement.Domain.Entities.VisitorEntryExit;

namespace P360.VisitorManagement.Api.Contracts;

public sealed record CreateVisitorEntryExitRequest(
    string? Name,
    string? Type,
    string? Description,
    string? CreatedBy)
{
    public EntryExitEntity ToEntity()
    {
        return new EntryExitEntity
        {
            Name = Name,
            Type = Type,
            Description = Description,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public sealed record UpdateVisitorEntryExitRequest(
    string? Name,
    string? Type,
    string? Description)
{
    public void ApplyTo(EntryExitEntity entryExit)
    {
        entryExit.Name = Name;
        entryExit.Type = Type;
        entryExit.Description = Description;
    }
}

public sealed record VisitorEntryExitResponse(
    string Id,
    string Name,
    string Type,
    string Description,
    string CreatedBy,
    DateTime CreatedAt)
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
            entryExit.CreatedAt);
    }
}
