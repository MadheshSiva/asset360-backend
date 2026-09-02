using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Asset.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AssetLocation : BaseEntity
{
    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("current_location")]
    public string? CurrentLocation { get; set; }

    [BsonElement("gps_coordinates")]
    public string? GpsCoordinates { get; set; }

    [BsonElement("location_history")]
    public string? LocationHistory { get; set; }

    [BsonElement("zone_transitions")]
    public string? ZoneTransitions { get; set; }

    [BsonElement("last_seen_location")]
    public string? LastSeenLocation { get; set; }
}
