using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AlertType : BaseEntity
{
    [BsonElement("alert_type_id")]
    public string AlertTypeId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("alert_name")]
    public string AlertName { get; set; } = string.Empty;

    [BsonElement("alert_code")]
    public string AlertCode { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    [BsonElement("category")]
    public string Category { get; set; } = string.Empty;

    [BsonElement("severity")]
    public string Severity { get; set; } = string.Empty;

    [BsonElement("trigger_condition")]
    public string TriggerCondition { get; set; } = string.Empty;

    [BsonElement("notification_type")]
    public string NotificationType { get; set; } = string.Empty;
}
