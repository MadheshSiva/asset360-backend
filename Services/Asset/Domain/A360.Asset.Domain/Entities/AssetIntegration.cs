using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Asset.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AssetIntegration : BaseEntity
{
    [BsonElement("integration_id")]
    public string IntegrationId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("erp_id")]
    public string ErpId { get; set; } = string.Empty;

    [BsonElement("wms_reference")]
    public string WmsReference { get; set; } = string.Empty;

    [BsonElement("api_sync_status")]
    public string ApiSyncStatus { get; set; } = string.Empty;

    [BsonElement("last_sync_timestamp")]
    public DateTime? LastSyncTimestamp { get; set; }
}
