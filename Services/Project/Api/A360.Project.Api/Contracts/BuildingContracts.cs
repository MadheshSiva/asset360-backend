using BuildingEntity = A360.Project.Domain.Entities.Building;

namespace A360.Project.Api.Contracts;

public sealed record CreateBuildingRequest(
    string? ProjectId,
    string? CountryId,
    string? AreaId,
    string? BuildingName,
    string? Description,
    string? Latitude,
    string? Longitude,
    bool Status,
    string? CreatedBy,
    string? ClientId);

public sealed record UpdateBuildingRequest(
    string? BuildingName,
    string? Description,
    string? Latitude,
    string? Longitude,
    bool Status);

public sealed record BuildingResponse(
    string Id,
    string ProjectId,
    string CountryId,
    string AreaId,
    string BuildingName,
    string Description,
    string Latitude,
    string Longitude,
    bool Status,
    string CreatedBy,
    DateTime CreatedAt,
    string ClientId)
{
    public static BuildingResponse FromEntity(BuildingEntity building)
    {
        return new BuildingResponse(
            building.Id,
            building.ProjectId,
            building.CountryId,
            building.AreaId,
            building.BuildingName,
            building.Description,
            building.Latitude,
            building.Longitude,
            building.Status,
            building.CreatedBy,
            building.CreatedAt,
            building.ClientId);
    }
}
