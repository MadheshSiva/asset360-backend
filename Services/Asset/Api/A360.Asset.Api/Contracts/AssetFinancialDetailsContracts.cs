using AssetFinancialDetailsEntity = A360.Asset.Domain.Entities.AssetFinancialDetails;

namespace A360.Asset.Api.Contracts;

public sealed record CreateAssetFinancialDetailsRequest(
    string? AssetId,
    string? AssetName,
    double PurchaseCost,
    DateTime? PurchaseDate,
    string? VendorDetails,
    string? InvoiceNumber,
    string? DepreciationMethod,
    double CurrentBookValue,
    double ResidualValue,
    string? CostCenterAllocation,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetFinancialDetailsEntity ToEntity(string financialDetailsId)
    {
        return new AssetFinancialDetailsEntity
        {
            FinancialDetailsId = financialDetailsId,
            AssetId = AssetId ?? string.Empty,
            AssetName = AssetName ?? string.Empty,
            PurchaseCost = PurchaseCost,
            PurchaseDate = PurchaseDate,
            VendorDetails = VendorDetails ?? string.Empty,
            InvoiceNumber = InvoiceNumber ?? string.Empty,
            DepreciationMethod = DepreciationMethod ?? string.Empty,
            CurrentBookValue = CurrentBookValue,
            ResidualValue = ResidualValue,
            CostCenterAllocation = CostCenterAllocation ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssetFinancialDetailsRequest(
    string? AssetId,
    string? AssetName,
    double PurchaseCost,
    DateTime? PurchaseDate,
    string? VendorDetails,
    string? InvoiceNumber,
    string? DepreciationMethod,
    double CurrentBookValue,
    double ResidualValue,
    string? CostCenterAllocation,
    string? UpdatedBy)
{
    public void ApplyTo(AssetFinancialDetailsEntity financialDetails)
    {
        financialDetails.AssetId = AssetId ?? string.Empty;
        financialDetails.AssetName = AssetName ?? string.Empty;
        financialDetails.PurchaseCost = PurchaseCost;
        financialDetails.PurchaseDate = PurchaseDate;
        financialDetails.VendorDetails = VendorDetails ?? string.Empty;
        financialDetails.InvoiceNumber = InvoiceNumber ?? string.Empty;
        financialDetails.DepreciationMethod = DepreciationMethod ?? string.Empty;
        financialDetails.CurrentBookValue = CurrentBookValue;
        financialDetails.ResidualValue = ResidualValue;
        financialDetails.CostCenterAllocation = CostCenterAllocation ?? string.Empty;
        financialDetails.UpdatedBy = UpdatedBy;
        financialDetails.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetFinancialDetailsResponse(
    string Id,
    string FinancialDetailsId,
    string AssetId,
    string AssetName,
    double PurchaseCost,
    DateTime? PurchaseDate,
    string VendorDetails,
    string InvoiceNumber,
    string DepreciationMethod,
    double CurrentBookValue,
    double ResidualValue,
    string CostCenterAllocation,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssetFinancialDetailsResponse FromEntity(AssetFinancialDetailsEntity financialDetails)
    {
        return new AssetFinancialDetailsResponse(
            financialDetails.Id,
            financialDetails.FinancialDetailsId,
            financialDetails.AssetId,
            financialDetails.AssetName,
            financialDetails.PurchaseCost,
            financialDetails.PurchaseDate,
            financialDetails.VendorDetails,
            financialDetails.InvoiceNumber,
            financialDetails.DepreciationMethod,
            financialDetails.CurrentBookValue,
            financialDetails.ResidualValue,
            financialDetails.CostCenterAllocation,
            financialDetails.CreatedBy,
            financialDetails.CreatedAt,
            financialDetails.UpdatedBy,
            financialDetails.UpdatedAt,
            financialDetails.ClientId,
            financialDetails.TenantId,
            financialDetails.IsDeleted);
    }
}
