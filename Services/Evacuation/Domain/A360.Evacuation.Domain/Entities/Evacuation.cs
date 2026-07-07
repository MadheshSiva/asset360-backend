
using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Evacuation.Domain.Entities;

public class Evacuation : BaseEntity
{
    [BsonElement("reference_id")]
    public string ReferenceId { get; set; } = null!;

    [BsonElement("project_id")]
    public string ProjectId { get; set; } = null!;

    [BsonElement("project_name")]
    public string ProjectName { get; set; } = null!;

    [BsonElement("country_id")]
    public string CountryId { get; set; } = null!;

    [BsonElement("country_name")]
    public string CountryName { get; set; } = null!;

    [BsonElement("area_id")]
    public string AreaId { get; set; } = null!;

    [BsonElement("area_name")]
    public string AreaName { get; set; } = null!;

    [BsonElement("building_id")]
    public string BuildingId { get; set; } = null!;

    [BsonElement("building_name")]
    public string BuildingName { get; set; } = null!;

    [BsonElement("floor_id")]
    public string FloorId { get; set; } = null!;

    [BsonElement("floor_name")]
    public string FloorName { get; set; } = null!;

    [BsonElement("zone_id")]
    public string ZoneId { get; set; } = null!;

    [BsonElement("zone_name")]
    public string ZoneName { get; set; } = null!;

    [BsonElement("camera_url")]
    public string? CameraUrl { get; set; }

    [BsonElement("camera_name")]
    public string? CameraName { get; set; }

    [BsonElement("created_by")]
    public string CreatedBy { get; set; } = null!;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("client_id")]
    public string ClientId { get; set; } = null!;
}
