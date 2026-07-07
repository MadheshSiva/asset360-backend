using MongoDB.Bson.Serialization.Attributes;
using P360.Domain.Entities;

namespace P360.VisitorManagement.Domain.Entities;

public class VisitorClientPermit : BaseEntity
{
    [BsonElement("client_name")]
    public string? ClientName { get; set; }

    [BsonElement("client_email")]
    public string? ClientEmail { get; set; }

    [BsonElement("support_contact_no")]
    public string? SupportContactNo { get; set; }

    [BsonElement("security_contact_no")]
    public string? SecurityContactNo { get; set; }

    [BsonElement("fire_contact_no")]
    public string? FireContactNo { get; set; }

    [BsonElement("created_by")]
    public string? CreatedBy { get; set; }

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }
}
