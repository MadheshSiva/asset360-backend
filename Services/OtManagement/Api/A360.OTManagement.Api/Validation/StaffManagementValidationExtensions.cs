using A360.OTManagement.Api.Contracts;

namespace A360.OTManagement.Api.Validation;

public static class StaffManagementValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreateStaffManagementRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.StaffId))
        {
            errors["StaffId"] =
                ["StaffId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.StaffName))
        {
            errors["StaffName"] =
                ["StaffName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Role))
        {
            errors["Role"] =
                ["Role is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Department))
        {
            errors["Department"] =
                ["Department is required"];
        }

        if (string.IsNullOrWhiteSpace(request.TagId))
        {
            errors["TagId"] =
                ["TagId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ContactNumber))
        {
            errors["ContactNumber"] =
                ["ContactNumber is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Shift))
        {
            errors["Shift"] =
                ["Shift is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdateStaffManagementRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.StaffName))
        {
            errors["StaffName"] =
                ["StaffName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Role))
        {
            errors["Role"] =
                ["Role is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Department))
        {
            errors["Department"] =
                ["Department is required"];
        }

        if (string.IsNullOrWhiteSpace(request.TagId))
        {
            errors["TagId"] =
                ["TagId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ContactNumber))
        {
            errors["ContactNumber"] =
                ["ContactNumber is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Shift))
        {
            errors["Shift"] =
                ["Shift is required"];
        }

        return errors;
    }
}