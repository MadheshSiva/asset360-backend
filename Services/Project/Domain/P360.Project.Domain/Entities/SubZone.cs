using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using P360.Domain.Entities;

namespace P360.Project.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class SubZone : BaseEntity
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

    [BsonElement("Zone_Id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ZoneId { get; set; } = string.Empty;

    [BsonElement("Sub_ZoneName")]
    public string SubZoneName { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    [BsonElement("top_zone")]
    public string TopZone { get; set; } = string.Empty;

    [BsonElement("priority")]
    public string Priority { get; set; } = string.Empty;

    [BsonElement("assembly_point")]
    public bool AssemblyPoint { get; set; }

    [BsonElement("exit")]
    public string Exit { get; set; } = string.Empty;

    [BsonElement("status")]
    public bool Status { get; set; }

    [BsonElement("created_by")]
    public string CreatedBy { get; set; } = string.Empty;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("client_id")]
    public string ClientId { get; set; } = string.Empty;

    [BsonElement("Time_Taken_Assemble_point")]
    public string TimeTakenAssemblePoint { get; set; } = string.Empty;

    [BsonElement("map_path")]
    public string MapPath { get; set; } = string.Empty;
}
