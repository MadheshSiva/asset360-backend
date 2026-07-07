
using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Evacuation.Domain.Entities;

public class EvacuationTrigger : BaseEntity
{
    [BsonElement("reference_id")]
    public string ReferenceId { get; set; } = null!;

    [BsonElement("trigger_field")]
    public string TriggerField { get; set; } = null!;

    [BsonElement("ip_address")]
    public string? IpAddress { get; set; }

    [BsonElement("application_name")]
    public string? ApplicationName { get; set; }

    [BsonElement("created_by")]
    public string CreatedBy { get; set; } = null!;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("client_id")]
    public string ClientId { get; set; } = null!;
}
