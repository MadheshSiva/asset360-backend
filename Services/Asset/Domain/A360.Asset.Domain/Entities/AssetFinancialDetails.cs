using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Asset.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AssetFinancialDetails : BaseEntity
{
    [BsonElement("financial_details_id")]
    public string FinancialDetailsId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("purchase_cost")]
    public double PurchaseCost { get; set; }

    [BsonElement("purchase_date")]
    public DateTime? PurchaseDate { get; set; }

    [BsonElement("vendor_details")]
    public string VendorDetails { get; set; } = string.Empty;

    [BsonElement("invoice_number")]
    public string InvoiceNumber { get; set; } = string.Empty;

    [BsonElement("depreciation_method")]
    public string DepreciationMethod { get; set; } = string.Empty;

    [BsonElement("current_book_value")]
    public double CurrentBookValue { get; set; }

    [BsonElement("residual_value")]
    public double ResidualValue { get; set; }

    [BsonElement("cost_center_allocation")]
    public string CostCenterAllocation { get; set; } = string.Empty;
}
