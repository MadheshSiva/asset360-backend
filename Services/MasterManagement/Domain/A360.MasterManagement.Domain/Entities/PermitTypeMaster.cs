using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class PermitTypeMaster : BaseEntity
{
    [BsonElement("permit_type_id")]
    public string PermitTypeId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("permit_name")]
    public string PermitName { get; set; } = string.Empty;

    [BsonElement("validity_days")]
    public int ValidityDays { get; set; }

    [BsonElement("is_approval_required")]
    public bool IsApprovalRequired { get; set; }
}
