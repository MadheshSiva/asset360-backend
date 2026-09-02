using OTManagementEntity = A360.OTManagement.Domain.Entities.OTManagement;

namespace A360.OTManagement.Api.Contracts;

public sealed record CreateOTManagementRequest(
    string? UniqueId,
    string? OTName,
    string? Department,
    string? Floor,
    string? Capacity,
    string? Type,
    bool Status,
    string? Sterilization,
    string? AirPressure,
    string? Temperature,
    string? Humidity,
    string? Project,
    string? Country,
    string? Area,
    string? Building,
    string? Zone,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public OTManagementEntity ToEntity()
    {
        return new OTManagementEntity
        {
            UniqueId = UniqueId,
            OTName = OTName,
            Department = Department,
            Floor = Floor,
            Capacity = Capacity,
            Type = Type,
            Status = Status,
            Sterilization = Sterilization,
            AirPressure = AirPressure,
            Temperature = Temperature,
            Humidity = Humidity,
            Project = Project,
            Country = Country,
            Area = Area,
            Building = Building,
            Zone = Zone,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateOTManagementRequest(
    string? OTName,
    string? Department,
    string? Floor,
    string? Capacity,
    string? Type,
    bool Status,
    string? Sterilization,
    string? AirPressure,
    string? Temperature,
    string? Humidity,
    string? Project,
    string? Country,
    string? Area,
    string? Building,
    string? Zone,
    string? UpdatedBy)
{
    public void ApplyTo(
        OTManagementEntity otManagement)
    {
        otManagement.OTName = OTName;
        otManagement.Department = Department;
        otManagement.Floor = Floor;
        otManagement.Capacity = Capacity;
        otManagement.Type = Type;
        otManagement.Status = Status;
        otManagement.Sterilization = Sterilization;
        otManagement.AirPressure = AirPressure;
        otManagement.Temperature = Temperature;
        otManagement.Humidity = Humidity;
        otManagement.Project = Project;
        otManagement.Country = Country;
        otManagement.Area = Area;
        otManagement.Building = Building;
        otManagement.Zone = Zone;
        otManagement.UpdatedBy = UpdatedBy;
        otManagement.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record OTManagementResponse(
    string Id,
    string UniqueId,
    string OTName,
    string Department,
    string Floor,
    string Capacity,
    string Type,
    bool Status,
    string Sterilization,
    string AirPressure,
    string Temperature,
    string Humidity,
    string Project,
    string Country,
    string Area,
    string Building,
    string Zone,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static OTManagementResponse FromEntity(
        OTManagementEntity otManagement)
    {
        return new OTManagementResponse(
            otManagement.Id ?? string.Empty,
            otManagement.UniqueId ?? string.Empty,
            otManagement.OTName ?? string.Empty,
            otManagement.Department ?? string.Empty,
            otManagement.Floor ?? string.Empty,
            otManagement.Capacity ?? string.Empty,
            otManagement.Type ?? string.Empty,
            otManagement.Status,
            otManagement.Sterilization ?? string.Empty,
            otManagement.AirPressure ?? string.Empty,
            otManagement.Temperature ?? string.Empty,
            otManagement.Humidity ?? string.Empty,
            otManagement.Project ?? string.Empty,
            otManagement.Country ?? string.Empty,
            otManagement.Area ?? string.Empty,
            otManagement.Building ?? string.Empty,
            otManagement.Zone ?? string.Empty,
            otManagement.CreatedBy,
            otManagement.CreatedAt,
            otManagement.UpdatedBy,
            otManagement.UpdatedAt,
            otManagement.ClientId,
            otManagement.TenantId,
            otManagement.IsDeleted);
    }
}