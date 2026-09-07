using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class Supplier : BaseEntity
{
    [BsonElement("supplier_code")]
    public string SupplierCode { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("supplier_name")]
    public string SupplierName { get; set; } = string.Empty;

    [BsonElement("contact_person")]
    public string ContactPerson { get; set; } = string.Empty;

    [BsonElement("email")]
    public string Email { get; set; } = string.Empty;

    [BsonElement("phone")]
    public string Phone { get; set; } = string.Empty;

    [BsonElement("address")]
    public string Address { get; set; } = string.Empty;

    [BsonElement("contract_reference")]
    public string ContractReference { get; set; } = string.Empty;
}
