using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class ModuleAccessMasterValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateModuleAccessMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ModuleName))
        {
            errors["ModuleName"] = ["ModuleName is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateModuleAccessMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ModuleName))
        {
            errors["ModuleName"] = ["ModuleName is required"];
        }

        return errors;
    }
}
