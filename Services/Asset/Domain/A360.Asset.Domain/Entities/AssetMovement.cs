using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Asset.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AssetMovement : BaseEntity
{
    [BsonElement("movement_id")]
    public string MovementId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("reference_number")]
    public string ReferenceNumber { get; set; } = string.Empty;

    [BsonElement("movement_status")]
    public string MovementStatus { get; set; } = string.Empty;

    [BsonElement("movement_date")]
    public DateTime? MovementDate { get; set; }

    [BsonElement("last_approval_workflow")]
    public string LastApprovalWorkflow { get; set; } = string.Empty;

    [BsonElement("next_approval_workflow")]
    public string NextApprovalWorkflow { get; set; } = string.Empty;
}
