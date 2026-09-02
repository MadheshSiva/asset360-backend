using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class Priority : BaseEntity
{
    [BsonElement("priority_id")]
    public string PriorityId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("priority_name")]
    public string PriorityName { get; set; } = string.Empty;

    [BsonElement("color_code")]
    public string ColorCode { get; set; } = string.Empty;

    [BsonElement("sla_mapping")]
    public string SlaMapping { get; set; } = string.Empty;

    [BsonElement("is_active")]
    public bool IsActive { get; set; }
}
