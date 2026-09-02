using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class StatusMaster : BaseEntity
{
    [BsonElement("status_id")]
    public string StatusId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("status_name")]
    public string StatusName { get; set; } = string.Empty;

    [BsonElement("color_code")]
    public string ColorCode { get; set; } = string.Empty;

    [BsonElement("allowed_transitions")]
    public string AllowedTransitions { get; set; } = string.Empty;

    [BsonElement("is_active")]
    public bool IsActive { get; set; }
}
