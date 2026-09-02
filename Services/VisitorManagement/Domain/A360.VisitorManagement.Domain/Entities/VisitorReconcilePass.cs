using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.VisitorManagement.Domain.Entities;

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
}
