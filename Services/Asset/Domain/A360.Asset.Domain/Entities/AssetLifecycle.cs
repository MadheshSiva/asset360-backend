using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Asset.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AssetLifecycle : BaseEntity
{
    [BsonElement("lifecycle_id")]
    public string LifecycleId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("procurement_date")]
    public DateTime? ProcurementDate { get; set; }

    [BsonElement("deployment_date")]
    public DateTime? DeploymentDate { get; set; }

    [BsonElement("lifecycle_status")]
    public string LifecycleStatus { get; set; } = "Active";

    [BsonElement("disposal_details")]
    public string DisposalDetails { get; set; } = string.Empty;

    [BsonElement("reason_for_retirement")]
    public string ReasonForRetirement { get; set; } = string.Empty;
}
