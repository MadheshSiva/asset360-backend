using MongoDB.Bson.Serialization.Attributes;
using P360.Domain.Entities;

namespace P360.OTManagement.Domain.Entities;

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
    public bool Status { get; set; }

    [BsonElement("created_by")]
    public string CreatedBy { get; set; } = null!;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }
}