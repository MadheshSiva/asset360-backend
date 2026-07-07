using P360.OTManagement.Api.Contracts;

namespace P360.OTManagement.Api.Validation;

public static class OTManagementValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreateOTManagementRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.UniqueId))
        {
            errors["UniqueId"] =
                ["UniqueId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.OTName))
        {
            errors["OTName"] =
                ["OTName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Department))
        {
            errors["Department"] =
                ["Department is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Floor))
        {
            errors["Floor"] =
                ["Floor is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Type))
        {
            errors["Type"] =
                ["Type is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdateOTManagementRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.OTName))
        {
            errors["OTName"] =
                ["OTName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Department))
        {
            errors["Department"] =
                ["Department is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Floor))
        {
            errors["Floor"] =
                ["Floor is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Type))
        {
            errors["Type"] =
                ["Type is required"];
        }

        return errors;
    }
}