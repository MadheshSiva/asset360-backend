using MongoDB.Bson.Serialization.Attributes;
using P360.Domain.Entities;

namespace P360.VisitorManagement.Domain.Entities;

public class VisitorReconcilePass : BaseEntity
{
    [BsonElement("number_Of_visitors")]
    public string? NumberOfVisitors { get; set; }

    [BsonElement("number_Of_pepole_exited")]
    public string? NumberOfPeopleExited { get; set; }

    [BsonElement("visitor_physically_present")]
    public string? VisitorPhysicallyPresent { get; set; }

    [BsonElement("Verified_security_emp_no")]
    public string? VerifiedSecurityEmpNo { get; set; }

    [BsonElement("created_by")]
    public string? CreatedBy { get; set; }

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }
}
