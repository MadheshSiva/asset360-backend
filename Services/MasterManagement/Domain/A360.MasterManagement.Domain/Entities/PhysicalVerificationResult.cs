using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class PhysicalVerificationResult : BaseEntity
{
    [BsonElement("result_id")]
    public string ResultId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("result_name")]
    public string ResultName { get; set; } = string.Empty;

    [BsonElement("result_code")]
    public string ResultCode { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    [BsonElement("result_category")]
    public string ResultCategory { get; set; } = string.Empty;

    [BsonElement("requires_action")]
    public bool RequiresAction { get; set; }
}
