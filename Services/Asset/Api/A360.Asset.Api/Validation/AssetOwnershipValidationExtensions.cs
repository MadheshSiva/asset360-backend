using A360.Asset.Api.Contracts;

namespace A360.Asset.Api.Validation;

public static class AssetOwnershipValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateAssetOwnershipRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AssetName))
        {
            errors["AssetName"] = ["AssetName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AssignedCustodian))
        {
            errors["AssignedCustodian"] = ["AssignedCustodian is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Department))
        {
            errors["Department"] = ["Department is required"];
        }

        if (request.AssignmentEndDate.HasValue && request.AssignmentStartDate.HasValue
            && request.AssignmentEndDate < request.AssignmentStartDate)
        {
            errors["AssignmentEndDate"] = ["AssignmentEndDate cannot be earlier than AssignmentStartDate"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateAssetOwnershipRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AssetName))
        {
            errors["AssetName"] = ["AssetName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AssignedCustodian))
        {
            errors["AssignedCustodian"] = ["AssignedCustodian is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Department))
        {
            errors["Department"] = ["Department is required"];
        }

        if (request.AssignmentEndDate.HasValue && request.AssignmentStartDate.HasValue
            && request.AssignmentEndDate < request.AssignmentStartDate)
        {
            errors["AssignmentEndDate"] = ["AssignmentEndDate cannot be earlier than AssignmentStartDate"];
        }

        return errors;
    }
}
