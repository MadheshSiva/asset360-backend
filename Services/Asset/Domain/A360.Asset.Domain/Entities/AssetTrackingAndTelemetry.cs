using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Asset.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AssetTrackingAndTelemetry : BaseEntity
{
    [BsonElement("tracking_id")]
    public string TrackingId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("device_identifier")]
    public string DeviceIdentifier { get; set; } = string.Empty;

    [BsonElement("tag_ids")]
    public string TagIds { get; set; } = string.Empty;

    [BsonElement("movement_logs")]
    public string MovementLogs { get; set; } = string.Empty;

    [BsonElement("last_seen_timestamp")]
    public DateTime? LastSeenTimestamp { get; set; }

    [BsonElement("speed_route")]
    public string SpeedRoute { get; set; } = string.Empty;

    [BsonElement("sensor_data")]
    public string SensorData { get; set; } = string.Empty;
}
