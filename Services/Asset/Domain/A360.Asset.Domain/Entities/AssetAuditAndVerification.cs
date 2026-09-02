using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Asset.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AssetAuditAndVerification : BaseEntity
{
    [BsonElement("audit_verification_id")]
    public string AuditVerificationId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("audit_date")]
    public DateTime? AuditDate { get; set; }

    [BsonElement("auditor_details")]
    public string AuditorDetails { get; set; } = string.Empty;

    [BsonElement("physical_verification_result")]
    public string PhysicalVerificationResult { get; set; } = string.Empty;

    [BsonElement("discrepancies_found")]
    public string DiscrepanciesFound { get; set; } = string.Empty;

    [BsonElement("audit_history_logs")]
    public string AuditHistoryLogs { get; set; } = string.Empty;
}
