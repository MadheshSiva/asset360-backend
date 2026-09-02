using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Asset.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class Asset : BaseEntity
{
    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    [BsonElement("category_sub_category")]
    public string CategorySubCategory { get; set; } = string.Empty;

    [BsonElement("serial_number")]
    public string SerialNumber { get; set; } = string.Empty;

    [BsonElement("tag_ids")]
    public string TagIds { get; set; } = string.Empty;

    [BsonElement("asset_type")]
    public string AssetType { get; set; } = string.Empty;

    [BsonElement("parent_asset")]
    public string? ParentAsset { get; set; }
}
