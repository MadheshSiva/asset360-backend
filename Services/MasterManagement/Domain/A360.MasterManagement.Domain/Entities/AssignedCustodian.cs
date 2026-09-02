using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AssignedCustodian : BaseEntity
{
    [BsonElement("assigned_custodian_id")]
    public string AssignedCustodianId { get; set; } = string.Empty;

    [BsonElement("department_or_custodian")]
    public string DepartmentOrCustodian { get; set; } = string.Empty;

    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("custodian_id")]
    public string CustodianId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    [BsonElement("role")]
    public string Role { get; set; } = string.Empty;

    [BsonElement("department_code")]
    public string DepartmentCode { get; set; } = string.Empty;
}
