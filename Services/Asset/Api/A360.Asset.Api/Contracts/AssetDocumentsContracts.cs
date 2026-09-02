using AssetDocumentsEntity = A360.Asset.Domain.Entities.AssetDocuments;

namespace A360.Asset.Api.Contracts;

public sealed record CreateAssetDocumentsRequest(
    string? AssetId,
    string? AssetName,
    string? PurchaseInvoice,
    string? WarrantyCertificate,
    string? Manuals,
    string? Images,
    string? ComplianceCertificates,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetDocumentsEntity ToEntity(string documentId)
    {
        return new AssetDocumentsEntity
        {
            DocumentId = documentId,
            AssetId = AssetId ?? string.Empty,
            AssetName = AssetName ?? string.Empty,
            PurchaseInvoice = PurchaseInvoice ?? string.Empty,
            WarrantyCertificate = WarrantyCertificate ?? string.Empty,
            Manuals = Manuals ?? string.Empty,
            Images = Images ?? string.Empty,
            ComplianceCertificates = ComplianceCertificates ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssetDocumentsRequest(
    string? AssetId,
    string? AssetName,
    string? PurchaseInvoice,
    string? WarrantyCertificate,
    string? Manuals,
    string? Images,
    string? ComplianceCertificates,
    string? UpdatedBy)
{
    public void ApplyTo(AssetDocumentsEntity documents)
    {
        documents.AssetId = AssetId ?? string.Empty;
        documents.AssetName = AssetName ?? string.Empty;
        documents.PurchaseInvoice = PurchaseInvoice ?? string.Empty;
        documents.WarrantyCertificate = WarrantyCertificate ?? string.Empty;
        documents.Manuals = Manuals ?? string.Empty;
        documents.Images = Images ?? string.Empty;
        documents.ComplianceCertificates = ComplianceCertificates ?? string.Empty;
        documents.UpdatedBy = UpdatedBy;
        documents.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetDocumentsResponse(
    string Id,
    string DocumentId,
    string AssetId,
    string AssetName,
    string PurchaseInvoice,
    string WarrantyCertificate,
    string Manuals,
    string Images,
    string ComplianceCertificates,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssetDocumentsResponse FromEntity(AssetDocumentsEntity documents)
    {
        return new AssetDocumentsResponse(
            documents.Id,
            documents.DocumentId,
            documents.AssetId,
            documents.AssetName,
            documents.PurchaseInvoice,
            documents.WarrantyCertificate,
            documents.Manuals,
            documents.Images,
            documents.ComplianceCertificates,
            documents.CreatedBy,
            documents.CreatedAt,
            documents.UpdatedBy,
            documents.UpdatedAt,
            documents.ClientId,
            documents.TenantId,
            documents.IsDeleted);
    }
}
