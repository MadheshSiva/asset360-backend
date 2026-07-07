using MongoDB.Bson.Serialization.Attributes;

namespace P360.VisitorManagement.Domain.Entities;

public class VisitorGatePassDocument
{
    [BsonElement("DocType")]
    public string? DocType { get; set; }

    [BsonElement("DocNumber")]
    public string? DocNumber { get; set; }

    [BsonElement("ExpiresOn")]
    public string? ExpiresOn { get; set; }

    [BsonElement("Upload")]
    public string? Upload { get; set; }
}
