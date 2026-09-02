using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Asset.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AssetDisposal : BaseEntity
{
    [BsonElement("disposal_id")]
    public string DisposalId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("reference_number")]
    public string ReferenceNumber { get; set; } = string.Empty;

    [BsonElement("requested_by")]
    public string RequestedBy { get; set; } = string.Empty;

    [BsonElement("disposal_reason")]
    public string DisposalReason { get; set; } = string.Empty;

    [BsonElement("disposal_status")]
    public string DisposalStatus { get; set; } = string.Empty;

    [BsonElement("disposal_date")]
    public DateTime? DisposalDate { get; set; }

    [BsonElement("last_approval_workflow")]
    public string LastApprovalWorkflow { get; set; } = string.Empty;

    [BsonElement("next_approval_workflow")]
    public string NextApprovalWorkflow { get; set; } = string.Empty;
}
