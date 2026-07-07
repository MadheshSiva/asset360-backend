using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.People.Domain.Entities;

public class Access : BaseEntity
{
    [BsonElement("group_type")]
    public string GroupType { get; set; } = null!;

    [BsonElement("group_name")]
    public string GroupName { get; set; } = null!;

    [BsonElement("members")]
    public List<string> Members { get; set; } = [];

    [BsonElement("readers")]
    public List<string> Readers { get; set; } = [];

    [BsonElement("status")]
    public bool Status { get; set; }

    [BsonElement("from_datetime")]
    public DateTime FromDateTime { get; set; }

    [BsonElement("to_datetime")]
    public DateTime ToDateTime { get; set; }

    [BsonElement("created_by")]
    public string CreatedBy { get; set; } = null!;

    [BsonElement("client_id")]
    public string ClientId { get; set; } = null!;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }
}