using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Asset.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class TaggedAssets : BaseEntity
{
    [BsonElement("tagged_asset_id")]
    public string TaggedAssetId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("asset_code")]
    public string AssetCode { get; set; } = string.Empty;

    [BsonElement("asset_description")]
    public string AssetDescription { get; set; } = string.Empty;

    [BsonElement("company")]
    public string Company { get; set; } = string.Empty;

    [BsonElement("site")]
    public string Site { get; set; } = string.Empty;

    [BsonElement("building")]
    public string Building { get; set; } = string.Empty;

    [BsonElement("floor")]
    public string Floor { get; set; } = string.Empty;

    [BsonElement("room")]
    public string Room { get; set; } = string.Empty;

    [BsonElement("main_category")]
    public string MainCategory { get; set; } = string.Empty;

    [BsonElement("sub_category")]
    public string SubCategory { get; set; } = string.Empty;

    [BsonElement("sub_sub_category")]
    public string SubSubCategory { get; set; } = string.Empty;

    [BsonElement("brand")]
    public string Brand { get; set; } = string.Empty;
}
