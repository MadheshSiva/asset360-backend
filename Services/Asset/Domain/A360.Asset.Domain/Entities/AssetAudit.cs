using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Asset.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AssetAudit : BaseEntity
{
    [BsonElement("audit_id")]
    public string AuditId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("audit_code")]
    public string AuditCode { get; set; } = string.Empty;

    [BsonElement("audit_name")]
    public string AuditName { get; set; } = string.Empty;

    [BsonElement("audit_start_date")]
    public DateTime? AuditStartDate { get; set; }

    [BsonElement("audit_end_date")]
    public DateTime? AuditEndDate { get; set; }

    [BsonElement("active")]
    public bool Active { get; set; }
}
