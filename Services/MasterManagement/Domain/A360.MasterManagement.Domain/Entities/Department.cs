using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class Department : BaseEntity
{
    [BsonElement("department_code")]
    public string DepartmentCode { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("department_name")]
    public string DepartmentName { get; set; } = string.Empty;

    [BsonElement("business_unit")]
    public string BusinessUnit { get; set; } = string.Empty;

    [BsonElement("department_head")]
    public string DepartmentHead { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;
}
