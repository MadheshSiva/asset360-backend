
using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Devices.Domain.Entities;

public class Device : BaseEntity
{
    [BsonElement("reference_id")]
    public string ReferenceId { get; set; } = null!;

    [BsonElement("model_id")]
    public string ModelId { get; set; } = null!;

    [BsonElement("type")]
    public string Type { get; set; } = null!;

    [BsonElement("unique_id")]
    public string UniqueId { get; set; } = null!;

    [BsonElement("technology")]
    public string Technology { get; set; } = null!;

    [BsonElement("project_id")]
    public string ProjectId { get; set; } = null!;

    [BsonElement("project_name")]
    public string ProjectName { get; set; } = null!;

    [BsonElement("description")]
    public string Description { get; set; } = null!;

    [BsonElement("building_id")]
    public string BuildingId { get; set; } = null!;

    [BsonElement("building_name")]
    public string BuildingName { get; set; } = null!;

    [BsonElement("floor_id")]
    public string FloorId { get; set; } = null!;

    [BsonElement("floor_name")]
    public string FloorName { get; set; } = null!;

    [BsonElement("area_id")]
    public string AreaId { get; set; } = null!;

    [BsonElement("area_name")]
    public string AreaName { get; set; } = null!;

    [BsonElement("zone_id")]
    public string ZoneId { get; set; } = null!;

    [BsonElement("zone_name")]
    public string ZoneName { get; set; } = null!;

    [BsonElement("country_id")]
    public string CountryId { get; set; } = null!;

    [BsonElement("country_name")]
    public string CountryName { get; set; } = null!;

    [BsonElement("mydevice_image")]
    public string? MydeviceImage { get; set; }

    [BsonElement("created_by")]
    public string CreatedBy { get; set; } = null!;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("client_id")]
    public string ClientId { get; set; } = null!;

    [BsonElement("flexi1")]
    public string? Flexi1 { get; set; }

    [BsonElement("flexi2")]
    public string? Flexi2 { get; set; }

    [BsonElement("flexi3")]
    public List<string>? Flexi3 { get; set; }

    [BsonElement("flexi4")]
    public string? Flexi4 { get; set; }

    [BsonElement("flexi5")]
    public string? Flexi5 { get; set; }

    [BsonElement("flexi6")]
    public string? Flexi6 { get; set; }

    [BsonElement("flexi7")]
    public string? Flexi7 { get; set; }

    [BsonElement("flexi8")]
    public string? Flexi8 { get; set; }

    [BsonElement("flexi9")]
    public string? Flexi9 { get; set; }

    [BsonElement("flexi10")]
    public string? Flexi10 { get; set; }

    [BsonElement("flexi11")]
    public string? Flexi11 { get; set; }

    [BsonElement("flexi12")]
    public string? Flexi12 { get; set; }

    [BsonElement("flexi13")]
    public string? Flexi13 { get; set; }

    [BsonElement("flexi14")]
    public string? Flexi14 { get; set; }

    [BsonElement("flexi15")]
    public string? Flexi15 { get; set; }

    [BsonElement("flexi16")]
    public string? Flexi16 { get; set; }

    [BsonElement("flexi17")]
    public string? Flexi17 { get; set; }

    [BsonElement("flexi18")]
    public string? Flexi18 { get; set; }

    [BsonElement("flexi19")]
    public string? Flexi19 { get; set; }

    [BsonElement("flexi20")]
    public string? Flexi20 { get; set; }

    [BsonElement("module")]
    public List<string>? Module { get; set; }

    [BsonElement("sensor_status")]
    public string? SensorStatus { get; set; }

    [BsonElement("status_updated_at")]
    public DateTime? StatusUpdatedAt { get; set; }

    [BsonElement("MyDeviceReferenceId")]
    public string? MyDeviceReferenceId { get; set; }
}