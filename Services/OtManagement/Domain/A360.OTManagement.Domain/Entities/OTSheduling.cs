using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.OTManagement.Domain.Entities;

public class OTScheduling : BaseEntity
{
    [BsonElement("schedule_id")]
    public string ScheduleId { get; set; } = null!;

    [BsonElement("resource_id")]
    public string ResourceId { get; set; } = null!;

    [BsonElement("surgeon")]
    public string Surgeon { get; set; } = null!;

    [BsonElement("start_time")]
    public DateTime StartTime { get; set; }

    [BsonElement("end_time")]
    public DateTime EndTime { get; set; }

    [BsonElement("surgery_type")]
    public string SurgeryType { get; set; } = null!;

    [BsonElement("priority")]
    public string Priority { get; set; } = null!;

    [BsonElement("status")]
    public bool Status { get; set; }

    [BsonElement("created_by")]
    public string CreatedBy { get; set; } = null!;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }
}