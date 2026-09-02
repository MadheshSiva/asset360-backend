using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class ConditionMaster : BaseEntity
{
    [BsonElement("condition_id")]
    public string ConditionId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("condition_name")]
    public string ConditionName { get; set; } = string.Empty;

    [BsonElement("threshold_value")]
    public double ThresholdValue { get; set; }

    [BsonElement("color_code")]
    public string ColorCode { get; set; } = string.Empty;
}
