using MongoDB.Bson.Serialization.Attributes;

namespace A360.VisitorManagement.Domain.Entities;

public class VisitorGatePassAssignAccessTransaction
{
    [BsonElement("action")]
    public string? Action { get; set; }

    [BsonElement("accessname")]
    public string? AccessName { get; set; }

    [BsonElement("created_by")]
    public string? CreatedBy { get; set; }

    [BsonElement("created_on")]
    public DateTime CreatedOn { get; set; }
}
