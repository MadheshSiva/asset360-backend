using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class StatusChange : BaseEntity
{
    [BsonElement("status_change_id")]
    public string StatusChangeId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("status_name")]
    public string StatusName { get; set; } = string.Empty;

    [BsonElement("status_code")]
    public string StatusCode { get; set; } = string.Empty;

    [BsonElement("sequence_order")]
    public int SequenceOrder { get; set; }

    [BsonElement("is_closed_status")]
    public bool IsClosedStatus { get; set; }

    [BsonElement("allowed_transitions")]
    public string AllowedTransitions { get; set; } = string.Empty;

    [BsonElement("requires_approval")]
    public bool RequiresApproval { get; set; }

    [BsonElement("is_default")]
    public bool IsDefault { get; set; }
}
