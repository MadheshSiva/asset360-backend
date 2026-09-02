using DepreciationMethodEntity = A360.MasterManagement.Domain.Entities.DepreciationMethod;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateDepreciationMethodRequest(
    string? MethodName,
    string? MethodCode,
    string? Description,
    string? CalculationType,
    double RatePercentage,
    int UsefulLifeYears,
    string? Status,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public DepreciationMethodEntity ToEntity(string methodId)
    {
        return new DepreciationMethodEntity
        {
            MethodId = methodId,
            MethodName = MethodName ?? string.Empty,
            MethodCode = MethodCode ?? string.Empty,
            Description = Description ?? string.Empty,
            CalculationType = CalculationType ?? string.Empty,
            RatePercentage = RatePercentage,
            UsefulLifeYears = UsefulLifeYears,
            Status = Status,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateDepreciationMethodRequest(
    string? MethodName,
    string? MethodCode,
    string? Description,
    string? CalculationType,
    double RatePercentage,
    int UsefulLifeYears,
    string? Status,
    string? UpdatedBy)
{
    public void ApplyTo(DepreciationMethodEntity depreciationMethod)
    {
        depreciationMethod.MethodName = MethodName ?? string.Empty;
        depreciationMethod.MethodCode = MethodCode ?? string.Empty;
        depreciationMethod.Description = Description ?? string.Empty;
        depreciationMethod.CalculationType = CalculationType ?? string.Empty;
        depreciationMethod.RatePercentage = RatePercentage;
        depreciationMethod.UsefulLifeYears = UsefulLifeYears;
        depreciationMethod.Status = Status;
        depreciationMethod.UpdatedBy = UpdatedBy;
        depreciationMethod.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record DepreciationMethodResponse(
    string Id,
    string MethodId,
    string MethodName,
    string MethodCode,
    string Description,
    string CalculationType,
    double RatePercentage,
    int UsefulLifeYears,
    string? Status,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static DepreciationMethodResponse FromEntity(DepreciationMethodEntity depreciationMethod)
    {
        return new DepreciationMethodResponse(
            depreciationMethod.Id,
            depreciationMethod.MethodId,
            depreciationMethod.MethodName,
            depreciationMethod.MethodCode,
            depreciationMethod.Description,
            depreciationMethod.CalculationType,
            depreciationMethod.RatePercentage,
            depreciationMethod.UsefulLifeYears,
            depreciationMethod.Status,
            depreciationMethod.CreatedBy,
            depreciationMethod.CreatedAt,
            depreciationMethod.UpdatedBy,
            depreciationMethod.UpdatedAt,
            depreciationMethod.ClientId,
            depreciationMethod.TenantId,
            depreciationMethod.IsDeleted);
    }
}
