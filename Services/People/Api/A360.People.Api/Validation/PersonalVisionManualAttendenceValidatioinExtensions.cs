using A360.People.Api.Contracts;

namespace A360.People.Api.Validation;

public static class PersonalVisionManualAttendanceValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreatePersonalVisionManualAttendanceRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.EmployeeId))
        {
            errors["EmployeeId"] =
                ["EmployeeId is required"];
        }

        if (request.FromDate == default)
        {
            errors["FromDate"] =
                ["FromDate is required"];
        }

        if (string.IsNullOrWhiteSpace(request.FromTime))
        {
            errors["FromTime"] =
                ["FromTime is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AttendanceStatus))
        {
            errors["AttendanceStatus"] =
                ["AttendanceStatus is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Reason))
        {
            errors["Reason"] =
                ["Reason is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdatePersonalVisionManualAttendanceRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.EmployeeId))
        {
            errors["EmployeeId"] =
                ["EmployeeId is required"];
        }

        if (request.FromDate == default)
        {
            errors["FromDate"] =
                ["FromDate is required"];
        }

        if (string.IsNullOrWhiteSpace(request.FromTime))
        {
            errors["FromTime"] =
                ["FromTime is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AttendanceStatus))
        {
            errors["AttendanceStatus"] =
                ["AttendanceStatus is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Reason))
        {
            errors["Reason"] =
                ["Reason is required"];
        }

        return errors;
    }
}