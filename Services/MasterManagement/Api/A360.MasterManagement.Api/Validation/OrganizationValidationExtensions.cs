using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class OrganizationValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateOrganizationRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.OrganizationName))
        {
            errors["OrganizationName"] = ["OrganizationName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.LegalName))
        {
            errors["LegalName"] = ["LegalName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            errors["Email"] = ["Email is required"];
        }

        if (string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            errors["PhoneNumber"] = ["PhoneNumber is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateOrganizationRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.OrganizationName))
        {
            errors["OrganizationName"] = ["OrganizationName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.LegalName))
        {
            errors["LegalName"] = ["LegalName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            errors["Email"] = ["Email is required"];
        }

        if (string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            errors["PhoneNumber"] = ["PhoneNumber is required"];
        }

        return errors;
    }
}
