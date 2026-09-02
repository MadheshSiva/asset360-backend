using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Asset.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AssetContract : BaseEntity
{
    [BsonElement("contract_id")]
    public string ContractId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("warranty_start_date")]
    public DateTime? WarrantyStartDate { get; set; }

    [BsonElement("warranty_end_date")]
    public DateTime? WarrantyEndDate { get; set; }

    [BsonElement("amc_details")]
    public string AmcDetails { get; set; } = string.Empty;

    [BsonElement("sla_details")]
    public string SlaDetails { get; set; } = string.Empty;

    [BsonElement("vendor_contract_documents")]
    public string VendorContractDocuments { get; set; } = string.Empty;
}
