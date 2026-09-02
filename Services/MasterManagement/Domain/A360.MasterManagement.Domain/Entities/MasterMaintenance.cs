using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class MasterMaintenance : BaseEntity
{
    [BsonElement("master_maintenance_id")]
    public string MasterMaintenanceId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("master_maintenance_code")]
    public string MasterMaintenanceCode { get; set; } = string.Empty;

    [BsonElement("master_maintenance_name")]
    public string MasterMaintenanceName { get; set; } = string.Empty;

    [BsonElement("category")]
    public string Category { get; set; } = string.Empty;

    [BsonElement("frequency")]
    public string Frequency { get; set; } = string.Empty;

    [BsonElement("standard_duration_hrs")]
    public double StandardDurationHrs { get; set; }

    [BsonElement("active")]
    public bool Active { get; set; }
}
