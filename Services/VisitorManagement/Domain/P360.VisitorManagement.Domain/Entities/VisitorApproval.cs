using MongoDB.Bson.Serialization.Attributes;
using P360.Domain.Entities;

namespace P360.VisitorManagement.Domain.Entities;

public class VisitorApproval : BaseEntity
{
    [BsonElement("created_by")]
    public string? CreatedBy { get; set; }

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("precedence")]
    public string? Precedence { get; set; }

    [BsonElement("PermitType")]
    public string? PermitType { get; set; }

    [BsonElement("EmployeeEmailID")]
    public List<string> EmployeeEmailIds { get; set; } = [];
}
