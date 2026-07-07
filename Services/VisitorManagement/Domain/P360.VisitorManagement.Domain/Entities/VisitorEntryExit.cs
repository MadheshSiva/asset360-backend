using MongoDB.Bson.Serialization.Attributes;
using P360.Domain.Entities;

namespace P360.VisitorManagement.Domain.Entities;

public class VisitorEntryExit : BaseEntity
{
    [BsonElement("name")]
    public string? Name { get; set; }

    [BsonElement("type")]
    public string? Type { get; set; }

    [BsonElement("description")]
    public string? Description { get; set; }

    [BsonElement("created_by")]
    public string? CreatedBy { get; set; }

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }
}
