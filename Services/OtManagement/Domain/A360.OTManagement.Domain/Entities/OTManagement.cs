using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.OTManagement.Domain.Entities;

public class OTManagement : BaseEntity
{
    [BsonElement("unique_id")]
    public string UniqueId { get; set; } = null!;

    [BsonElement("ot_name")]
    public string OTName { get; set; } = null!;

    [BsonElement("department")]
    public string Department { get; set; } = null!;

    [BsonElement("floor")]
    public string Floor { get; set; } = null!;

    [BsonElement("capacity")]
    public string Capacity { get; set; } = null!;

    [BsonElement("type")]
    public string Type { get; set; } = null!;

    [BsonElement("status")]
    public bool Status { get; set; }

    [BsonElement("sterilization")]
    public string Sterilization { get; set; } = null!;

    [BsonElement("air_pressure")]
    public string AirPressure { get; set; } = null!;

    [BsonElement("temperature")]
    public string Temperature { get; set; } = null!;

    [BsonElement("humidity")]
    public string Humidity { get; set; } = null!;

    [BsonElement("project")]
    public string Project { get; set; } = null!;

    [BsonElement("country")]
    public string Country { get; set; } = null!;

    [BsonElement("area")]
    public string Area { get; set; } = null!;

    [BsonElement("building")]
    public string Building { get; set; } = null!;

    [BsonElement("zone")]
    public string Zone { get; set; } = null!;

    [BsonElement("created_by")]
    public string CreatedBy { get; set; } = null!;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }
}