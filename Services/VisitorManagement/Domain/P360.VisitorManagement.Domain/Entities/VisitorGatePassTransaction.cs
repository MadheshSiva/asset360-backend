using MongoDB.Bson.Serialization.Attributes;

namespace P360.VisitorManagement.Domain.Entities;

public class VisitorGatePassTransaction
{
    [BsonElement("description")]
    public string? Description { get; set; }

    [BsonElement("levelstatus")]
    public string? LevelStatus { get; set; }

    [BsonElement("created_by")]
    public string? CreatedBy { get; set; }

    [BsonElement("created_on")]
    public DateTime CreatedOn { get; set; }
}
