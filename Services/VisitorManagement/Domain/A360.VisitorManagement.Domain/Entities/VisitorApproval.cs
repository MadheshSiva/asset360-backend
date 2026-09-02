using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.VisitorManagement.Domain.Entities;

public class VisitorApproval : BaseEntity
{
    [BsonElement("precedence")]
    public string? Precedence { get; set; }

    [BsonElement("PermitType")]
    public string? PermitType { get; set; }

    [BsonElement("EmployeeEmailID")]
    public List<string> EmployeeEmailIds { get; set; } = [];
}
