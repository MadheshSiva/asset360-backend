using MongoDB.Bson.Serialization.Attributes;
using P360.Domain.Entities;

namespace P360.VisitorManagement.Domain.Entities;

public class EmailTemplate : BaseEntity
{
    [BsonElement("name")]
    public string? Name { get; set; }

    [BsonElement("subject")]
    public string? Subject { get; set; }

    [BsonElement("body")]
    public string? Body { get; set; }
}
