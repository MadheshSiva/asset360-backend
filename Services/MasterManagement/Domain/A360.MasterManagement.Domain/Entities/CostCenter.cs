using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class CostCenter : BaseEntity
{
    [BsonElement("cost_center_id")]
    public string CostCenterId { get; set; } = string.Empty;

    [BsonElement("cost_center_name")]
    public string CostCenterName { get; set; } = string.Empty;

    [BsonElement("cost_center_code")]
    public string CostCenterCode { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    [BsonElement("department")]
    public string Department { get; set; } = string.Empty;

    [BsonElement("parent_cost_center")]
    public string? ParentCostCenter { get; set; }

    [BsonElement("manager")]
    public string Manager { get; set; } = string.Empty;

    [BsonElement("budget_amount")]
    public decimal BudgetAmount { get; set; }
}
