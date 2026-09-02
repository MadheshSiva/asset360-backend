using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.OTManagement.Domain.Entities;

public class PatientMaster : BaseEntity
{
    [BsonElement("his_id")]
    public string HisId { get; set; } = null!;

    [BsonElement("patient_name")]
    public string PatientName { get; set; } = null!;

    [BsonElement("gender")]
    public string Gender { get; set; } = null!;

    [BsonElement("case_id")]
    public string CaseId { get; set; } = null!;

    [BsonElement("department")]
    public string Department { get; set; } = null!;

    [BsonElement("priority")]
    public string Priority { get; set; } = null!;

    [BsonElement("surgery_type")]
    public string SurgeryType { get; set; } = null!;

    [BsonElement("status")]
    public new bool Status { get; set; }
}