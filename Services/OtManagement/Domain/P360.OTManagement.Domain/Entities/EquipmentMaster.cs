using MongoDB.Bson.Serialization.Attributes;
using P360.Domain.Entities;

namespace P360.OTManagement.Domain.Entities;

public class EquipmentMaster : BaseEntity
{
    [BsonElement("asset_id")]
    public string AssetId { get; set; } = null!;

    [BsonElement("equipment_name")]
    public string EquipmentName { get; set; } = null!;

    [BsonElement("type")]
    public string Type { get; set; } = null!;

    [BsonElement("serial_number")]
    public string SerialNumber { get; set; } = null!;

    [BsonElement("location")]
    public string Location { get; set; } = null!;

    [BsonElement("tag_id")]
    public string TagId { get; set; } = null!;

    [BsonElement("service_date")]
    public DateTime ServiceDate { get; set; }

    [BsonElement("status")]
    public bool Status { get; set; }

    [BsonElement("created_by")]
    public string CreatedBy { get; set; } = null!;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }
}