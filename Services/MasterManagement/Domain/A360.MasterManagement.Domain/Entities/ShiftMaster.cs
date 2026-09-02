using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class ShiftMaster : BaseEntity
{
    [BsonElement("shift_id")]
    public string ShiftId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("shift_name")]
    public string ShiftName { get; set; } = string.Empty;

    [BsonElement("start_time")]
    public string StartTime { get; set; } = string.Empty;

    [BsonElement("end_time")]
    public string EndTime { get; set; } = string.Empty;
}
