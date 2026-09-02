using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class Category : BaseEntity
{
    [BsonElement("category_id")]
    public string CategoryId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("category_name")]
    public string CategoryName { get; set; } = string.Empty;

    [BsonElement("category_code")]
    public string CategoryCode { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    [BsonElement("level")]
    public string Level { get; set; } = string.Empty;

    [BsonElement("related_asset")]
    public string? RelatedAsset { get; set; }
}
