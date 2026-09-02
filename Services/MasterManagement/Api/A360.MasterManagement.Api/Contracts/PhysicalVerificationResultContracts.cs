using PhysicalVerificationResultEntity = A360.MasterManagement.Domain.Entities.PhysicalVerificationResult;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreatePhysicalVerificationResultRequest(
    string? AssetId,
    string? ResultName,
    string? ResultCode,
    string? Description,
    string? ResultCategory,
    bool RequiresAction,
    string? Status,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public PhysicalVerificationResultEntity ToEntity(string resultId, string assetName)
    {
        return new PhysicalVerificationResultEntity
        {
            ResultId = resultId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            ResultName = ResultName ?? string.Empty,
            ResultCode = ResultCode ?? string.Empty,
            Description = Description ?? string.Empty,
            ResultCategory = ResultCategory ?? string.Empty,
            RequiresAction = RequiresAction,
            Status = Status,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdatePhysicalVerificationResultRequest(
    string? AssetId,
    string? ResultName,
    string? ResultCode,
    string? Description,
    string? ResultCategory,
    bool RequiresAction,
    string? Status,
    string? UpdatedBy)
{
    public void ApplyTo(PhysicalVerificationResultEntity physicalVerificationResult, string assetName)
    {
        physicalVerificationResult.AssetId = AssetId ?? string.Empty;
        physicalVerificationResult.AssetName = assetName;
        physicalVerificationResult.ResultName = ResultName ?? string.Empty;
        physicalVerificationResult.ResultCode = ResultCode ?? string.Empty;
        physicalVerificationResult.Description = Description ?? string.Empty;
        physicalVerificationResult.ResultCategory = ResultCategory ?? string.Empty;
        physicalVerificationResult.RequiresAction = RequiresAction;
        physicalVerificationResult.Status = Status;
        physicalVerificationResult.UpdatedBy = UpdatedBy;
        physicalVerificationResult.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record PhysicalVerificationResultResponse(
    string Id,
    string ResultId,
    string AssetId,
    string AssetName,
    string ResultName,
    string ResultCode,
    string Description,
    string ResultCategory,
    bool RequiresAction,
    string? Status,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static PhysicalVerificationResultResponse FromEntity(PhysicalVerificationResultEntity physicalVerificationResult)
    {
        return new PhysicalVerificationResultResponse(
            physicalVerificationResult.Id,
            physicalVerificationResult.ResultId,
            physicalVerificationResult.AssetId,
            physicalVerificationResult.AssetName,
            physicalVerificationResult.ResultName,
            physicalVerificationResult.ResultCode,
            physicalVerificationResult.Description,
            physicalVerificationResult.ResultCategory,
            physicalVerificationResult.RequiresAction,
            physicalVerificationResult.Status,
            physicalVerificationResult.CreatedBy,
            physicalVerificationResult.CreatedAt,
            physicalVerificationResult.UpdatedBy,
            physicalVerificationResult.UpdatedAt,
            physicalVerificationResult.ClientId,
            physicalVerificationResult.TenantId,
            physicalVerificationResult.IsDeleted);
    }
}
