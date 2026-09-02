using AssetCheckoutEntity = A360.Asset.Domain.Entities.AssetCheckout;

namespace A360.Asset.Api.Contracts;

public sealed record CreateAssetCheckoutRequest(
    string? AssetId,
    string? AssetName,
    string? AssetCode,
    string? AssetDescription,
    string? Company,
    string? Site,
    string? Building,
    string? Floor,
    string? Room,
    string? DepartmentName,
    string? CustodianName,
    string? MainCategory,
    string? SubCategory,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetCheckoutEntity ToEntity(string checkoutId)
    {
        return new AssetCheckoutEntity
        {
            CheckoutId = checkoutId,
            AssetId = AssetId ?? string.Empty,
            AssetName = AssetName ?? string.Empty,
            AssetCode = AssetCode ?? string.Empty,
            AssetDescription = AssetDescription ?? string.Empty,
            Company = Company ?? string.Empty,
            Site = Site ?? string.Empty,
            Building = Building ?? string.Empty,
            Floor = Floor ?? string.Empty,
            Room = Room ?? string.Empty,
            DepartmentName = DepartmentName ?? string.Empty,
            CustodianName = CustodianName ?? string.Empty,
            MainCategory = MainCategory ?? string.Empty,
            SubCategory = SubCategory ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssetCheckoutRequest(
    string? AssetId,
    string? AssetName,
    string? AssetCode,
    string? AssetDescription,
    string? Company,
    string? Site,
    string? Building,
    string? Floor,
    string? Room,
    string? DepartmentName,
    string? CustodianName,
    string? MainCategory,
    string? SubCategory,
    string? UpdatedBy)
{
    public void ApplyTo(AssetCheckoutEntity checkout)
    {
        checkout.AssetId = AssetId ?? string.Empty;
        checkout.AssetName = AssetName ?? string.Empty;
        checkout.AssetCode = AssetCode ?? string.Empty;
        checkout.AssetDescription = AssetDescription ?? string.Empty;
        checkout.Company = Company ?? string.Empty;
        checkout.Site = Site ?? string.Empty;
        checkout.Building = Building ?? string.Empty;
        checkout.Floor = Floor ?? string.Empty;
        checkout.Room = Room ?? string.Empty;
        checkout.DepartmentName = DepartmentName ?? string.Empty;
        checkout.CustodianName = CustodianName ?? string.Empty;
        checkout.MainCategory = MainCategory ?? string.Empty;
        checkout.SubCategory = SubCategory ?? string.Empty;
        checkout.UpdatedBy = UpdatedBy;
        checkout.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetCheckoutResponse(
    string Id,
    string CheckoutId,
    string AssetId,
    string AssetName,
    string AssetCode,
    string AssetDescription,
    string Company,
    string Site,
    string Building,
    string Floor,
    string Room,
    string DepartmentName,
    string CustodianName,
    string MainCategory,
    string SubCategory,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssetCheckoutResponse FromEntity(AssetCheckoutEntity checkout)
    {
        return new AssetCheckoutResponse(
            checkout.Id,
            checkout.CheckoutId,
            checkout.AssetId,
            checkout.AssetName,
            checkout.AssetCode,
            checkout.AssetDescription,
            checkout.Company,
            checkout.Site,
            checkout.Building,
            checkout.Floor,
            checkout.Room,
            checkout.DepartmentName,
            checkout.CustodianName,
            checkout.MainCategory,
            checkout.SubCategory,
            checkout.CreatedBy,
            checkout.CreatedAt,
            checkout.UpdatedBy,
            checkout.UpdatedAt,
            checkout.ClientId,
            checkout.TenantId,
            checkout.IsDeleted);
    }
}
