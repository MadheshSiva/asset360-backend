using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Asset.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AssetIncident : BaseEntity
{
    [BsonElement("incident_id")]
    public string IncidentId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("alert_type")]
    public string AlertType { get; set; } = string.Empty;

    [BsonElement("incident_reports")]
    public string IncidentReports { get; set; } = string.Empty;

    [BsonElement("damage_reports")]
    public string DamageReports { get; set; } = string.Empty;

    [BsonElement("theft_loss_records")]
    public string TheftLossRecords { get; set; } = string.Empty;

    [BsonElement("resolution_status")]
    public string ResolutionStatus { get; set; } = string.Empty;
}
