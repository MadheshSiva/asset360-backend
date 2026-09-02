using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.People.Domain.Entities;

public class Group : BaseEntity
{
    [BsonElement("group_type")]
    public string GroupType { get; set; } = null!;

    [BsonElement("group_name")]
    public string GroupName { get; set; } = null!;

    [BsonElement("members")]
    public List<string> Members { get; set; } = [];

    [BsonElement("action")]
    public string? Action { get; set; }
}