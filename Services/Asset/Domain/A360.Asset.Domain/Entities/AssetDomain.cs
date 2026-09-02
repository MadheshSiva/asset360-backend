using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Asset.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AssetDomain : BaseEntity
{
    [BsonElement("asset_domain_id")]
    public string AssetDomainId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("asset_type")]
    public string AssetType { get; set; } = string.Empty;

    [BsonElement("field_name")]
    public string FieldName { get; set; } = string.Empty;

    [BsonElement("field_value")]
    public string FieldValue { get; set; } = string.Empty;
}
