using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.VisitorManagement.Domain.Entities;

public class VisitorEntryExit : BaseEntity
{
    [BsonElement("name")]
    public string? Name { get; set; }

    [BsonElement("type")]
    public string? Type { get; set; }

    [BsonElement("description")]
    public string? Description { get; set; }
}
