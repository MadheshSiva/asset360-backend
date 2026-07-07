

using MongoDB.Bson.Serialization.Attributes;

namespace A360.VisitorManagement.Domain.Entities;

public class VisitorRegistrationDocument
{
    [BsonElement("documentType")]
    public string? DocumentType { get; set; }

    [BsonElement("documentNumber")]
    public string? DocumentNumber { get; set; }

    [BsonElement("expiresOn")]
    public string? ExpiresOn { get; set; }

    [BsonElement("documentUrl")]
    public string? DocumentUrl { get; set; }
}
