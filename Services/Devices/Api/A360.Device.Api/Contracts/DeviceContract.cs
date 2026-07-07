
using DeviceEntity = A360.Devices.Domain.Entities.Device;

namespace A360.Devices.Api.Contracts;

public sealed record CreateDeviceRequest(
    string? ReferenceId,
    string? ModelId,
    string? Type,
    string? UniqueId,
    string? Technology,
    string? ProjectId,
    string? ProjectName,
    string? Description,
    string? BuildingId,
    string? BuildingName,
    string? FloorId,
    string? FloorName,
    string? AreaId,
    string? AreaName,
    string? ZoneId,
    string? ZoneName,
    string? CountryId,
    string? CountryName,
    string? MydeviceImage,
    string? CreatedBy,
    string? ClientId,
    string? Flexi1,
    string? Flexi2,
    List<string>? Flexi3,
    string? Flexi4,
    string? Flexi5,
    string? Flexi6,
    string? Flexi7,
    string? Flexi8,
    string? Flexi9,
    string? Flexi10,
    string? Flexi11,
    string? Flexi12,
    string? Flexi13,
    string? Flexi14,
    string? Flexi15,
    string? Flexi16,
    string? Flexi17,
    string? Flexi18,
    string? Flexi19,
    string? Flexi20,
    List<string>? Module)
{
    public DeviceEntity ToEntity()
    {
        return new DeviceEntity
        {
            ReferenceId = ReferenceId,
            ModelId = ModelId,
            Type = Type,
            UniqueId = UniqueId,
            Technology = Technology,

            ProjectId = ProjectId,
            ProjectName = ProjectName,

            Description = Description,

            BuildingId = BuildingId,
            BuildingName = BuildingName,

            FloorId = FloorId,
            FloorName = FloorName,

            AreaId = AreaId,
            AreaName = AreaName,

            ZoneId = ZoneId,
            ZoneName = ZoneName,

            CountryId = CountryId,
            CountryName = CountryName,

            MydeviceImage = MydeviceImage,

            CreatedBy = CreatedBy,
            ClientId = ClientId,

            Flexi1 = Flexi1,
            Flexi2 = Flexi2,
            Flexi3 = Flexi3 ?? new List<string>(),
            Flexi4 = Flexi4,
            Flexi5 = Flexi5,
            Flexi6 = Flexi6,
            Flexi7 = Flexi7,
            Flexi8 = Flexi8,
            Flexi9 = Flexi9,
            Flexi10 = Flexi10,
            Flexi11 = Flexi11,
            Flexi12 = Flexi12,
            Flexi13 = Flexi13,
            Flexi14 = Flexi14,
            Flexi15 = Flexi15,
            Flexi16 = Flexi16,
            Flexi17 = Flexi17,
            Flexi18 = Flexi18,
            Flexi19 = Flexi19,
            Flexi20 = Flexi20,

            Module = Module ?? new List<string>(),

            CreatedAt = DateTime.UtcNow
        };
    }
}

public sealed record UpdateDeviceRequest(
    string? ModelId,
    string? Type,
    string? UniqueId,
    string? Technology,
    string? ProjectId,
    string? ProjectName,
    string? Description,
    string? BuildingId,
    string? BuildingName,
    string? FloorId,
    string? FloorName,
    string? AreaId,
    string? AreaName,
    string? ZoneId,
    string? ZoneName,
    string? CountryId,
    string? CountryName,
    string? MydeviceImage,
    string? Flexi1,
    string? Flexi2,
    List<string>? Flexi3,
    string? Flexi4,
    string? Flexi5,
    string? Flexi6,
    string? Flexi7,
    string? Flexi8,
    string? Flexi9,
    string? Flexi10,
    string? Flexi11,
    string? Flexi12,
    string? Flexi13,
    string? Flexi14,
    string? Flexi15,
    string? Flexi16,
    string? Flexi17,
    string? Flexi18,
    string? Flexi19,
    string? Flexi20,
    List<string>? Module)
{
    public void ApplyTo(DeviceEntity device)
    {
        device.ModelId = ModelId;
        device.Type = Type;
        device.UniqueId = UniqueId;
        device.Technology = Technology;

        device.ProjectId = ProjectId;
        device.ProjectName = ProjectName;

        device.Description = Description;

        device.BuildingId = BuildingId;
        device.BuildingName = BuildingName;

        device.FloorId = FloorId;
        device.FloorName = FloorName;

        device.AreaId = AreaId;
        device.AreaName = AreaName;

        device.ZoneId = ZoneId;
        device.ZoneName = ZoneName;

        device.CountryId = CountryId;
        device.CountryName = CountryName;

        device.MydeviceImage = MydeviceImage;

        device.Flexi1 = Flexi1;
        device.Flexi2 = Flexi2;
        device.Flexi3 = Flexi3 ?? new List<string>();
        device.Flexi4 = Flexi4;
        device.Flexi5 = Flexi5;
        device.Flexi6 = Flexi6;
        device.Flexi7 = Flexi7;
        device.Flexi8 = Flexi8;
        device.Flexi9 = Flexi9;
        device.Flexi10 = Flexi10;
        device.Flexi11 = Flexi11;
        device.Flexi12 = Flexi12;
        device.Flexi13 = Flexi13;
        device.Flexi14 = Flexi14;
        device.Flexi15 = Flexi15;
        device.Flexi16 = Flexi16;
        device.Flexi17 = Flexi17;
        device.Flexi18 = Flexi18;
        device.Flexi19 = Flexi19;
        device.Flexi20 = Flexi20;

        device.Module = Module ?? new List<string>();
    }
}

public sealed record DeviceResponse(
    string Id,
    string ReferenceId,
    string ModelId,
    string Type,
    string UniqueId,
    string Technology,
    string ProjectId,
    string ProjectName,
    string Description,
    string BuildingId,
    string BuildingName,
    string FloorId,
    string FloorName,
    string AreaId,
    string AreaName,
    string ZoneId,
    string ZoneName,
    string CountryId,
    string CountryName,
    string MydeviceImage,
    string CreatedBy,
    DateTime CreatedAt,
    string ClientId,
    string? Flexi1,
    string? Flexi2,
    List<string>? Flexi3,
    string? Flexi4,
    string? Flexi5,
    string? Flexi6,
    string? Flexi7,
    string? Flexi8,
    string? Flexi9,
    string? Flexi10,
    string? Flexi11,
    string? Flexi12,
    string? Flexi13,
    string? Flexi14,
    string? Flexi15,
    string? Flexi16,
    string? Flexi17,
    string? Flexi18,
    string? Flexi19,
    string? Flexi20,
    List<string>? Module)
{
    public static DeviceResponse FromEntity(DeviceEntity device)
    {
        return new DeviceResponse(
            device.Id ?? string.Empty,
            device.ReferenceId ?? string.Empty,
            device.ModelId ?? string.Empty,
            device.Type ?? string.Empty,
            device.UniqueId ?? string.Empty,
            device.Technology ?? string.Empty,

            device.ProjectId ?? string.Empty,
            device.ProjectName ?? string.Empty,

            device.Description ?? string.Empty,

            device.BuildingId ?? string.Empty,
            device.BuildingName ?? string.Empty,

            device.FloorId ?? string.Empty,
            device.FloorName ?? string.Empty,

            device.AreaId ?? string.Empty,
            device.AreaName ?? string.Empty,

            device.ZoneId ?? string.Empty,
            device.ZoneName ?? string.Empty,

            device.CountryId ?? string.Empty,
            device.CountryName ?? string.Empty,

            device.MydeviceImage ?? string.Empty,

            device.CreatedBy ?? string.Empty,
            device.CreatedAt,

            device.ClientId ?? string.Empty,

            device.Flexi1,
            device.Flexi2,
            device.Flexi3,
            device.Flexi4,
            device.Flexi5,
            device.Flexi6,
            device.Flexi7,
            device.Flexi8,
            device.Flexi9,
            device.Flexi10,
            device.Flexi11,
            device.Flexi12,
            device.Flexi13,
            device.Flexi14,
            device.Flexi15,
            device.Flexi16,
            device.Flexi17,
            device.Flexi18,
            device.Flexi19,
            device.Flexi20,

            device.Module
        );
    }
}
