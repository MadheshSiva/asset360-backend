using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Asset.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AssetActivity : BaseEntity
{
    [BsonElement("activity_id")]
    public string ActivityId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("who_created_updated_asset")]
    public string WhoCreatedUpdatedAsset { get; set; } = string.Empty;

    [BsonElement("changes_made")]
    public string ChangesMade { get; set; } = string.Empty;

    [BsonElement("timestamp_logs")]
    public string TimestampLogs { get; set; } = string.Empty;

    [BsonElement("access_logs")]
    public string AccessLogs { get; set; } = string.Empty;
}
