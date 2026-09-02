
using EvacuationEntity = A360.Evacuation.Domain.Entities.Evacuation;

namespace A360.Evacuation.Api.Contracts;

public sealed record CreateEvacuationRequest(
    string? ReferenceId,
    string? ProjectId,
    string? ProjectName,
    string? CountryId,
    string? CountryName,
    string? AreaId,
    string? AreaName,
    string? BuildingId,
    string? BuildingName,
    string? FloorId,
    string? FloorName,
    string? ZoneId,
    string? ZoneName,
    string? CameraUrl,
    string? CameraName,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public EvacuationEntity ToEntity()
    {
        return new EvacuationEntity
        {
            ReferenceId = ReferenceId,

            ProjectId = ProjectId,
            ProjectName = ProjectName,

            CountryId = CountryId,
            CountryName = CountryName,

            AreaId = AreaId,
            AreaName = AreaName,

            BuildingId = BuildingId,
            BuildingName = BuildingName,

            FloorId = FloorId,
            FloorName = FloorName,

            ZoneId = ZoneId,
            ZoneName = ZoneName,

            CameraUrl = CameraUrl,
            CameraName = CameraName,

            CreatedBy = CreatedBy,
            ClientId = ClientId,
            TenantId = TenantId,

            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
    }
}

public sealed record UpdateEvacuationRequest(
    string? ProjectId,
    string? ProjectName,
    string? CountryId,
    string? CountryName,
    string? AreaId,
    string? AreaName,
    string? BuildingId,
    string? BuildingName,
    string? FloorId,
    string? FloorName,
    string? ZoneId,
    string? ZoneName,
    string? CameraUrl,
    string? CameraName,
    string? UpdatedBy,
    string? Status)
{
    public void ApplyTo(EvacuationEntity evacuation)
    {
        evacuation.ProjectId = ProjectId;
        evacuation.ProjectName = ProjectName;

        evacuation.CountryId = CountryId;
        evacuation.CountryName = CountryName;

        evacuation.AreaId = AreaId;
        evacuation.AreaName = AreaName;

        evacuation.BuildingId = BuildingId;
        evacuation.BuildingName = BuildingName;

        evacuation.FloorId = FloorId;
        evacuation.FloorName = FloorName;

        evacuation.ZoneId = ZoneId;
        evacuation.ZoneName = ZoneName;

        evacuation.CameraUrl = CameraUrl;
        evacuation.CameraName = CameraName;

        evacuation.UpdatedBy = UpdatedBy;
        evacuation.UpdatedAt = DateTime.UtcNow;
        if (!string.IsNullOrWhiteSpace(Status))
        {
            evacuation.Status = Status;
        }
    }
}

public sealed record EvacuationResponse(
    string Id,
    string ReferenceId,
    string ProjectId,
    string ProjectName,
    string CountryId,
    string CountryName,
    string AreaId,
    string AreaName,
    string BuildingId,
    string BuildingName,
    string FloorId,
    string FloorName,
    string ZoneId,
    string ZoneName,
    string? CameraUrl,
    string? CameraName,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? ClientId,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? TenantId,
    string? Status,
    bool IsDeleted)
{
    public static EvacuationResponse FromEntity(EvacuationEntity evacuation)
    {
        return new EvacuationResponse(
            evacuation.Id ?? string.Empty,
            evacuation.ReferenceId ?? string.Empty,

            evacuation.ProjectId ?? string.Empty,
            evacuation.ProjectName ?? string.Empty,

            evacuation.CountryId ?? string.Empty,
            evacuation.CountryName ?? string.Empty,

            evacuation.AreaId ?? string.Empty,
            evacuation.AreaName ?? string.Empty,

            evacuation.BuildingId ?? string.Empty,
            evacuation.BuildingName ?? string.Empty,

            evacuation.FloorId ?? string.Empty,
            evacuation.FloorName ?? string.Empty,

            evacuation.ZoneId ?? string.Empty,
            evacuation.ZoneName ?? string.Empty,

            evacuation.CameraUrl,
            evacuation.CameraName,

            evacuation.CreatedBy,
            evacuation.CreatedAt,

            evacuation.ClientId,

            evacuation.UpdatedBy,
            evacuation.UpdatedAt,
            evacuation.TenantId,
            evacuation.Status,
            evacuation.IsDeleted
        );
    }
}
