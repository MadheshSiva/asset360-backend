using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Asset.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AssetCheckin : BaseEntity
{
    [BsonElement("checkin_id")]
    public string CheckinId { get; set; } = string.Empty;

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

    [BsonElement("department_name")]
    public string DepartmentName { get; set; } = string.Empty;

    [BsonElement("custodian_name")]
    public string CustodianName { get; set; } = string.Empty;
}
