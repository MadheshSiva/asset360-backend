using FloorEntity = A360.Project.Domain.Entities.Floor;

namespace A360.Project.Api.Contracts;

public sealed record CreateFloorRequest(
    string? ProjectId,
    string? CountryId,
    string? AreaId,
    string? OuterZoneId,
    string? BuildingId,
    string? FloorName,
    string? Description,
    bool Status,
    string? CreatedBy,
    string? ClientId,
    string? TenantId,
    string? MapPath);

public sealed record UpdateFloorRequest(
    string? FloorName,
    string? Description,
    bool Status,
    string? MapPath,
    string? UpdatedBy);

public sealed record FloorResponse(
    string Id,
    string ProjectId,
    string CountryId,
    string AreaId,
    string OuterZoneId,
    string BuildingId,
    string FloorName,
    string Description,
    bool Status,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted,
    string MapPath)
{
    public static FloorResponse FromEntity(FloorEntity floor)
    {
        return new FloorResponse(
            floor.Id,
            floor.ProjectId,
            floor.CountryId,
            floor.AreaId,
            floor.OuterZoneId,
            floor.BuildingId,
            floor.FloorName,
            floor.Description,
            floor.Status,
            floor.CreatedBy,
            floor.CreatedAt,
            floor.UpdatedBy,
            floor.UpdatedAt,
            floor.ClientId,
            floor.TenantId,
            floor.IsDeleted,
            floor.MapPath);
    }
}

public sealed record FloorMapResponse(string Id, string MapPath);
