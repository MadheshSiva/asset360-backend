using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Asset.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AssetMaintenanceAndService : BaseEntity
{
    [BsonElement("maintenance_service_id")]
    public string MaintenanceServiceId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("maintenance_schedule")]
    public string MaintenanceSchedule { get; set; } = string.Empty;

    [BsonElement("work_orders")]
    public string WorkOrders { get; set; } = string.Empty;

    [BsonElement("service_history")]
    public string ServiceHistory { get; set; } = string.Empty;

    [BsonElement("repair_logs")]
    public string RepairLogs { get; set; } = string.Empty;

    [BsonElement("downtime_duration")]
    public string DowntimeDuration { get; set; } = string.Empty;

    [BsonElement("spare_parts_used")]
    public string SparePartsUsed { get; set; } = string.Empty;

    [BsonElement("vendor_service_provider_details")]
    public string VendorServiceProviderDetails { get; set; } = string.Empty;
}
