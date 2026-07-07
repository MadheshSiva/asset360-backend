using P360.OTManagement.Api.Contracts;

namespace P360.OTManagement.Api.Validation;

public static class EquipmentMasterValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreateEquipmentMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] =
                ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.EquipmentName))
        {
            errors["EquipmentName"] =
                ["EquipmentName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Type))
        {
            errors["Type"] =
                ["Type is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Location))
        {
            errors["Location"] =
                ["Location is required"];
        }

        if (string.IsNullOrWhiteSpace(request.TagId))
        {
            errors["TagId"] =
                ["TagId is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdateEquipmentMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.EquipmentName))
        {
            errors["EquipmentName"] =
                ["EquipmentName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Type))
        {
            errors["Type"] =
                ["Type is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Location))
        {
            errors["Location"] =
                ["Location is required"];
        }

        if (string.IsNullOrWhiteSpace(request.TagId))
        {
            errors["TagId"] =
                ["TagId is required"];
        }

        return errors;
    }
}
