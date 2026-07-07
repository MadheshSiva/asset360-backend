using A360.OTManagement.Api.Contracts;

namespace A360.OTManagement.Api.Validation;

public static class OTSchedulingValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreateOTSchedulingRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.ScheduleId))
        {
            errors["ScheduleId"] =
                ["ScheduleId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ResourceId))
        {
            errors["ResourceId"] =
                ["ResourceId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Surgeon))
        {
            errors["Surgeon"] =
                ["Surgeon is required"];
        }

        if (request.StartTime == default)
        {
            errors["StartTime"] =
                ["StartTime is required"];
        }

        if (request.EndTime == default)
        {
            errors["EndTime"] =
                ["EndTime is required"];
        }

        if (request.EndTime <= request.StartTime)
        {
            errors["EndTime"] =
                ["EndTime must be greater than StartTime"];
        }

        if (string.IsNullOrWhiteSpace(request.SurgeryType))
        {
            errors["SurgeryType"] =
                ["SurgeryType is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Priority))
        {
            errors["Priority"] =
                ["Priority is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdateOTSchedulingRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.ResourceId))
        {
            errors["ResourceId"] =
                ["ResourceId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Surgeon))
        {
            errors["Surgeon"] =
                ["Surgeon is required"];
        }

        if (request.StartTime == default)
        {
            errors["StartTime"] =
                ["StartTime is required"];
        }

        if (request.EndTime == default)
        {
            errors["EndTime"] =
                ["EndTime is required"];
        }

        if (request.EndTime <= request.StartTime)
        {
            errors["EndTime"] =
                ["EndTime must be greater than StartTime"];
        }

        if (string.IsNullOrWhiteSpace(request.SurgeryType))
        {
            errors["SurgeryType"] =
                ["SurgeryType is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Priority))
        {
            errors["Priority"] =
                ["Priority is required"];
        }

        return errors;
    }
}