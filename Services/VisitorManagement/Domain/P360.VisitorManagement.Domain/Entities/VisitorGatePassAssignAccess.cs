using MongoDB.Bson.Serialization.Attributes;

namespace P360.VisitorManagement.Domain.Entities;

public class VisitorGatePassAssignAccess
{
    [BsonElement("accessName")]
    public string? AccessName { get; set; }

    [BsonElement("accessId")]
    public string? AccessId { get; set; }
}
