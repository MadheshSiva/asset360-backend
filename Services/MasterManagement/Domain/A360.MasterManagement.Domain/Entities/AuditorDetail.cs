using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AuditorDetail : BaseEntity
{
    [BsonElement("auditor_id")]
    public string AuditorId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("auditor_name")]
    public string AuditorName { get; set; } = string.Empty;

    [BsonElement("employee_code")]
    public string EmployeeCode { get; set; } = string.Empty;

    [BsonElement("department")]
    public string Department { get; set; } = string.Empty;

    [BsonElement("email")]
    public string Email { get; set; } = string.Empty;

    [BsonElement("phone")]
    public string Phone { get; set; } = string.Empty;

    [BsonElement("certification_type")]
    public string CertificationType { get; set; } = string.Empty;
}
