using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class DepreciationMethod : BaseEntity
{
    [BsonElement("method_id")]
    public string MethodId { get; set; } = string.Empty;

    [BsonElement("method_name")]
    public string MethodName { get; set; } = string.Empty;

    [BsonElement("method_code")]
    public string MethodCode { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    [BsonElement("calculation_type")]
    public string CalculationType { get; set; } = string.Empty;

    [BsonElement("rate_percentage")]
    public double RatePercentage { get; set; }

    [BsonElement("useful_life_years")]
    public int UsefulLifeYears { get; set; }
}
