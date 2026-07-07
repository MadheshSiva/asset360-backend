using A360.OTManagement.Api.Contracts;

namespace A360.OTManagement.Api.Validation;

public static class PatientMasterValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreatePatientMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.HisId))
        {
            errors["HisId"] =
                ["HisId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.PatientName))
        {
            errors["PatientName"] =
                ["PatientName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Gender))
        {
            errors["Gender"] =
                ["Gender is required"];
        }

        if (string.IsNullOrWhiteSpace(request.CaseId))
        {
            errors["CaseId"] =
                ["CaseId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Department))
        {
            errors["Department"] =
                ["Department is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Priority))
        {
            errors["Priority"] =
                ["Priority is required"];
        }

        if (string.IsNullOrWhiteSpace(request.SurgeryType))
        {
            errors["SurgeryType"] =
                ["SurgeryType is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdatePatientMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.PatientName))
        {
            errors["PatientName"] =
                ["PatientName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Gender))
        {
            errors["Gender"] =
                ["Gender is required"];
        }

        if (string.IsNullOrWhiteSpace(request.CaseId))
        {
            errors["CaseId"] =
                ["CaseId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Department))
        {
            errors["Department"] =
                ["Department is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Priority))
        {
            errors["Priority"] =
                ["Priority is required"];
        }

        if (string.IsNullOrWhiteSpace(request.SurgeryType))
        {
            errors["SurgeryType"] =
                ["SurgeryType is required"];
        }

        return errors;
    }
}