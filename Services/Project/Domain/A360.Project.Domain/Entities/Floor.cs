using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Project.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class Floor : BaseEntity
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

    [BsonElement("floor_name")]
    public string FloorName { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    [BsonElement("status")]
    public bool Status { get; set; }

    [BsonElement("created_by")]
    public string CreatedBy { get; set; } = string.Empty;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("client_id")]
    public string ClientId { get; set; } = string.Empty;

    [BsonElement("map_path")]
    public string MapPath { get; set; } = string.Empty;
}
