using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class ResolutionStatus : BaseEntity
{
    [BsonElement("status_id")]
    public string StatusId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("status_name")]
    public string StatusName { get; set; } = string.Empty;

    [BsonElement("status_code")]
    public string StatusCode { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    [BsonElement("is_final_status")]
    public bool IsFinalStatus { get; set; }

    [BsonElement("status_category")]
    public string StatusCategory { get; set; } = string.Empty;

    [BsonElement("sequence_order")]
    public int SequenceOrder { get; set; }

    [BsonElement("status_color")]
    public string StatusColor { get; set; } = string.Empty;
}
