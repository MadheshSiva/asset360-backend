using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class PermissionMaster : BaseEntity
{
    [BsonElement("permission_id")]
    public string PermissionId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("permission_name")]
    public string PermissionName { get; set; } = string.Empty;

    [BsonElement("module")]
    public string Module { get; set; } = string.Empty;
}
