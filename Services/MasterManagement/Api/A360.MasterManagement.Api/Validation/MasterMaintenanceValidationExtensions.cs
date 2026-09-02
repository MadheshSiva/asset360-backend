using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class MasterMaintenanceValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateMasterMaintenanceRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.MasterMaintenanceCode))
        {
            errors["MasterMaintenanceCode"] = ["MasterMaintenanceCode is required"];
        }

        if (string.IsNullOrWhiteSpace(request.MasterMaintenanceName))
        {
            errors["MasterMaintenanceName"] = ["MasterMaintenanceName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Category))
        {
            errors["Category"] = ["Category is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Frequency))
        {
            errors["Frequency"] = ["Frequency is required"];
        }

        if (request.StandardDurationHrs < 0)
        {
            errors["StandardDurationHrs"] = ["StandardDurationHrs cannot be negative"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateMasterMaintenanceRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.MasterMaintenanceCode))
        {
            errors["MasterMaintenanceCode"] = ["MasterMaintenanceCode is required"];
        }

        if (string.IsNullOrWhiteSpace(request.MasterMaintenanceName))
        {
            errors["MasterMaintenanceName"] = ["MasterMaintenanceName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Category))
        {
            errors["Category"] = ["Category is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Frequency))
        {
            errors["Frequency"] = ["Frequency is required"];
        }

        if (request.StandardDurationHrs < 0)
        {
            errors["StandardDurationHrs"] = ["StandardDurationHrs cannot be negative"];
        }

        return errors;
    }
}
