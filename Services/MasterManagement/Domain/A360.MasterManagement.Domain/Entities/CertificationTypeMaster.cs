using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class CertificationTypeMaster : BaseEntity
{
    [BsonElement("certification_id")]
    public string CertificationId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("certification_name")]
    public string CertificationName { get; set; } = string.Empty;

    [BsonElement("certification_code")]
    public string CertificationCode { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    [BsonElement("applicable_asset_type")]
    public string ApplicableAssetType { get; set; } = string.Empty;

    [BsonElement("issuing_authority")]
    public string IssuingAuthority { get; set; } = string.Empty;

    [BsonElement("validity_period_days")]
    public int ValidityPeriodDays { get; set; }

    [BsonElement("renewal_required")]
    public bool RenewalRequired { get; set; }
}
