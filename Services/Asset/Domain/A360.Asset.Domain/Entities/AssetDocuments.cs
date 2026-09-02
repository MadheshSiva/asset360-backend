using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Asset.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AssetDocuments : BaseEntity
{
    [BsonElement("document_id")]
    public string DocumentId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("purchase_invoice")]
    public string PurchaseInvoice { get; set; } = string.Empty;

    [BsonElement("warranty_certificate")]
    public string WarrantyCertificate { get; set; } = string.Empty;

    [BsonElement("manuals")]
    public string Manuals { get; set; } = string.Empty;

    [BsonElement("images")]
    public string Images { get; set; } = string.Empty;

    [BsonElement("compliance_certificates")]
    public string ComplianceCertificates { get; set; } = string.Empty;
}
