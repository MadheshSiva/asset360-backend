using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class SeverityMaster : BaseEntity
{
    [BsonElement("severity_id")]
    public string SeverityId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("severity_name")]
    public string SeverityName { get; set; } = string.Empty;

    [BsonElement("color_code")]
    public string ColorCode { get; set; } = string.Empty;
}
