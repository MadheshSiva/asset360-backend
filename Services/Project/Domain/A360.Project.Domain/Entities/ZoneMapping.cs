using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Project.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class ZoneMapping : BaseEntity
{
    [BsonElement("project_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ProjectId { get; set; } = string.Empty;

    [BsonElement("country_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string CountryId { get; set; } = string.Empty;

    [BsonElement("area_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string AreaId { get; set; } = string.Empty;

    [BsonElement("building_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string BuildingId { get; set; } = string.Empty;

    [BsonElement("floor_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string FloorId { get; set; } = string.Empty;

    [BsonElement("zone_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ZoneId { get; set; } = string.Empty;

    [BsonElement("zone_name")]
    public string ZoneName { get; set; } = string.Empty;

    [BsonElement("description")]
    public string? Description { get; set; }

    [BsonElement("top_zone")]
    public string? TopZone { get; set; }

    [BsonElement("priority")]
    public string? Priority { get; set; }

    [BsonElement("assembly_point")]
    public bool? AssemblyPoint { get; set; }

    [BsonElement("exit")]
    public string? Exit { get; set; }

    [BsonElement("status")]
    public bool Status { get; set; }

    [BsonElement("created_by")]
    public string CreatedBy { get; set; } = string.Empty;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("client_id")]
    public string ClientId { get; set; } = string.Empty;

    [BsonElement("zone_colour")]
    public string? ZoneColour { get; set; }

    [BsonElement("geoJsondata")]
    public List<BsonDocument> GeoJsonData { get; set; } = [];
}
