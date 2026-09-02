using AssetContractEntity = A360.Asset.Domain.Entities.AssetContract;

namespace A360.Asset.Api.Contracts;

public sealed record CreateAssetContractRequest(
    string? AssetId,
    string? AssetName,
    DateTime? WarrantyStartDate,
    DateTime? WarrantyEndDate,
    string? AmcDetails,
    string? SlaDetails,
    string? VendorContractDocuments,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetContractEntity ToEntity(string contractId)
    {
        return new AssetContractEntity
        {
            ContractId = contractId,
            AssetId = AssetId ?? string.Empty,
            AssetName = AssetName ?? string.Empty,
            WarrantyStartDate = WarrantyStartDate,
            WarrantyEndDate = WarrantyEndDate,
            AmcDetails = AmcDetails ?? string.Empty,
            SlaDetails = SlaDetails ?? string.Empty,
            VendorContractDocuments = VendorContractDocuments ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssetContractRequest(
    string? AssetId,
    string? AssetName,
    DateTime? WarrantyStartDate,
    DateTime? WarrantyEndDate,
    string? AmcDetails,
    string? SlaDetails,
    string? VendorContractDocuments,
    string? UpdatedBy)
{
    public void ApplyTo(AssetContractEntity contract)
    {
        contract.AssetId = AssetId ?? string.Empty;
        contract.AssetName = AssetName ?? string.Empty;
        contract.WarrantyStartDate = WarrantyStartDate;
        contract.WarrantyEndDate = WarrantyEndDate;
        contract.AmcDetails = AmcDetails ?? string.Empty;
        contract.SlaDetails = SlaDetails ?? string.Empty;
        contract.VendorContractDocuments = VendorContractDocuments ?? string.Empty;
        contract.UpdatedBy = UpdatedBy;
        contract.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetContractResponse(
    string Id,
    string ContractId,
    string AssetId,
    string AssetName,
    DateTime? WarrantyStartDate,
    DateTime? WarrantyEndDate,
    string AmcDetails,
    string SlaDetails,
    string VendorContractDocuments,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssetContractResponse FromEntity(AssetContractEntity contract)
    {
        return new AssetContractResponse(
            contract.Id,
            contract.ContractId,
            contract.AssetId,
            contract.AssetName,
            contract.WarrantyStartDate,
            contract.WarrantyEndDate,
            contract.AmcDetails,
            contract.SlaDetails,
            contract.VendorContractDocuments,
            contract.CreatedBy,
            contract.CreatedAt,
            contract.UpdatedBy,
            contract.UpdatedAt,
            contract.ClientId,
            contract.TenantId,
            contract.IsDeleted);
    }
}
