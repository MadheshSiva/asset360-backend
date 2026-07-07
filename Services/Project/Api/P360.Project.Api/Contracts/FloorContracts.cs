using FloorEntity = P360.Project.Domain.Entities.Floor;

namespace P360.Project.Api.Contracts;

public sealed record CreateFloorRequest(
    string? ProjectId,
    string? CountryId,
    string? AreaId,
    string? BuildingId,
    string? FloorName,
    string? Description,
    bool Status,
    string? CreatedBy,
    string? ClientId,
    string? MapPath);

public sealed record UpdateFloorRequest(
    string? FloorName,
    string? Description,
    bool Status,
    string? MapPath);

public sealed record FloorResponse(
    string Id,
    string ProjectId,
    string CountryId,
    string AreaId,
    string BuildingId,
    string FloorName,
    string Description,
    bool Status,
    string CreatedBy,
    DateTime CreatedAt,
    string ClientId,
    string MapPath)
{
    public static FloorResponse FromEntity(FloorEntity floor)
    {
        return new FloorResponse(
            floor.Id,
            floor.ProjectId,
            floor.CountryId,
            floor.AreaId,
            floor.BuildingId,
            floor.FloorName,
            floor.Description,
            floor.Status,
            floor.CreatedBy,
            floor.CreatedAt,
            floor.ClientId,
            floor.MapPath);
    }
}

public sealed record FloorMapResponse(string Id, string MapPath);
