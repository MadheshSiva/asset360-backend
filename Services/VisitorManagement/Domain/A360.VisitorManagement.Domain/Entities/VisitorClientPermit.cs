using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.VisitorManagement.Domain.Entities;

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
}
