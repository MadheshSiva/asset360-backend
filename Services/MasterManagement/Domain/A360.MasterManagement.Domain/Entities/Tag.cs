using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class Tag : BaseEntity
{
    [BsonElement("tag_id")]
    public string TagId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("tag_code")]
    public string TagCode { get; set; } = string.Empty;

    [BsonElement("tag_type")]
    public string TagType { get; set; } = string.Empty;

    [BsonElement("assigned_asset_code")]
    public string AssignedAssetCode { get; set; } = string.Empty;

    [BsonElement("issue_date")]
    public DateTime? IssueDate { get; set; }

    [BsonElement("active")]
    public bool Active { get; set; }
}
