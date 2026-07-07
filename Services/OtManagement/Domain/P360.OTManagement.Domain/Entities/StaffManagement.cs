using MongoDB.Bson.Serialization.Attributes;
using P360.Domain.Entities;

namespace P360.OTManagement.Domain.Entities;

public class StaffManagement : BaseEntity
{
    [BsonElement("staff_id")]
    public string StaffId { get; set; } = null!;

    [BsonElement("staff_name")]
    public string StaffName { get; set; } = null!;

    [BsonElement("role")]
    public string Role { get; set; } = null!;

    [BsonElement("department")]
    public string Department { get; set; } = null!;

    [BsonElement("tag_id")]
    public string TagId { get; set; } = null!;

    [BsonElement("contact_number")]
    public string ContactNumber { get; set; } = null!;

    [BsonElement("shift")]
    public string Shift { get; set; } = null!;

    [BsonElement("status")]
    public bool Status { get; set; }

    [BsonElement("created_by")]
    public string CreatedBy { get; set; } = null!;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }
}