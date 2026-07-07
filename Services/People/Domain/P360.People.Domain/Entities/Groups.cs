using MongoDB.Bson.Serialization.Attributes;
using P360.Domain.Entities;

namespace P360.People.Domain.Entities;

public class Group : BaseEntity
{
    [BsonElement("group_type")]
    public string GroupType { get; set; } = null!;

    [BsonElement("group_name")]
    public string GroupName { get; set; } = null!;

    [BsonElement("members")]
    public List<string> Members { get; set; } = [];

    [BsonElement("created_by")]
    public string CreatedBy { get; set; } = null!;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("client_id")]
    public string ClientId { get; set; } = null!;

    [BsonElement("action")]
    public string? Action { get; set; }
}