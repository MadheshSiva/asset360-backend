using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class SkillMaster : BaseEntity
{
    [BsonElement("skill_id")]
    public string SkillId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("skill_name")]
    public string SkillName { get; set; } = string.Empty;

    [BsonElement("skill_level")]
    public string SkillLevel { get; set; } = string.Empty;

    [BsonElement("certification_required")]
    public bool CertificationRequired { get; set; }
}
