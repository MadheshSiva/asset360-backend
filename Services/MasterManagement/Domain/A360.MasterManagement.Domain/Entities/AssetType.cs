using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AssetType : BaseEntity
{
    [BsonElement("asset_type_id")]
    public string AssetTypeId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("asset_type_name")]
    public string AssetTypeName { get; set; } = string.Empty;

    [BsonElement("asset_type_code")]
    public string AssetTypeCode { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;
}
