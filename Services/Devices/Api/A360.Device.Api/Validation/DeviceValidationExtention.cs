
using A360.Devices.Api.Contracts;

namespace A360.Devices.Api.Validation;

public static class DeviceValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreateDeviceRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.ModelId))
        {
            errors["ModelId"] =
                ["ModelId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Type))
        {
            errors["Type"] =
                ["Type is required"];
        }

        if (string.IsNullOrWhiteSpace(request.UniqueId))
        {
            errors["UniqueId"] =
                ["UniqueId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Technology))
        {
            errors["Technology"] =
                ["Technology is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ProjectId))
        {
            errors["ProjectId"] =
                ["ProjectId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ProjectName))
        {
            errors["ProjectName"] =
                ["ProjectName is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdateDeviceRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.ModelId))
        {
            errors["ModelId"] =
                ["ModelId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Type))
        {
            errors["Type"] =
                ["Type is required"];
        }

        if (string.IsNullOrWhiteSpace(request.UniqueId))
        {
            errors["UniqueId"] =
                ["UniqueId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Technology))
        {
            errors["Technology"] =
                ["Technology is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ProjectId))
        {
            errors["ProjectId"] =
                ["ProjectId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ProjectName))
        {
            errors["ProjectName"] =
                ["ProjectName is required"];
        }

        return errors;
    }
}
