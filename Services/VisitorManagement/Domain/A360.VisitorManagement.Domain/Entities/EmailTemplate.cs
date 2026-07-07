using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.VisitorManagement.Domain.Entities;

public class EmailTemplate : BaseEntity
{
    [BsonElement("name")]
    public string? Name { get; set; }

    [BsonElement("subject")]
    public string? Subject { get; set; }

    [BsonElement("body")]
    public string? Body { get; set; }
}
