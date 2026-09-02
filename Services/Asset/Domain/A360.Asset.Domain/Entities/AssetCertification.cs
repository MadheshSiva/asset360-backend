using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Asset.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AssetCertification : BaseEntity
{
    [BsonElement("certification_id")]
    public string CertificationId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("certification_type")]
    public string CertificationType { get; set; } = string.Empty;

    [BsonElement("issued_date")]
    public DateTime? IssuedDate { get; set; }

    [BsonElement("expiry_date")]
    public DateTime? ExpiryDate { get; set; }

    [BsonElement("inspection_logs")]
    public string InspectionLogs { get; set; } = string.Empty;
}
