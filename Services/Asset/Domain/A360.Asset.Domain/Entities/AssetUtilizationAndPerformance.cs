using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Asset.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AssetUtilizationAndPerformance : BaseEntity
{
    [BsonElement("utilization_performance_id")]
    public string UtilizationPerformanceId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("usage_hours")]
    public double UsageHours { get; set; }

    [BsonElement("idle_time")]
    public double IdleTime { get; set; }

    [BsonElement("movement_frequency")]
    public string MovementFrequency { get; set; } = string.Empty;

    [BsonElement("utilization_percentage")]
    public double UtilizationPercentage { get; set; }

    [BsonElement("productivity_metrics")]
    public string ProductivityMetrics { get; set; } = string.Empty;
}
