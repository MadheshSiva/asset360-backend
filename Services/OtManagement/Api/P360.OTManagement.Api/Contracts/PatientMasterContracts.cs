using PatientMasterEntity = P360.OTManagement.Domain.Entities.PatientMaster;

namespace P360.OTManagement.Api.Contracts;

public sealed record CreatePatientMasterRequest(
    string? HisId,
    string? PatientName,
    string? Gender,
    string? CaseId,
    string? Department,
    string? Priority,
    string? SurgeryType,
    bool Status,
    string? CreatedBy)
{
    public PatientMasterEntity ToEntity()
    {
        return new PatientMasterEntity
        {
            HisId = HisId,
            PatientName = PatientName,
            Gender = Gender,
            CaseId = CaseId,
            Department = Department,
            Priority = Priority,
            SurgeryType = SurgeryType,
            Status = Status,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public sealed record UpdatePatientMasterRequest(
    string? PatientName,
    string? Gender,
    string? CaseId,
    string? Department,
    string? Priority,
    string? SurgeryType,
    bool Status)
{
    public void ApplyTo(
        PatientMasterEntity patientMaster)
    {
        patientMaster.PatientName = PatientName;
        patientMaster.Gender = Gender;
        patientMaster.CaseId = CaseId;
        patientMaster.Department = Department;
        patientMaster.Priority = Priority;
        patientMaster.SurgeryType = SurgeryType;
        patientMaster.Status = Status;
    }
}

public sealed record PatientMasterResponse(
    string Id,
    string HisId,
    string PatientName,
    string Gender,
    string CaseId,
    string Department,
    string Priority,
    string SurgeryType,
    bool Status,
    string CreatedBy,
    DateTime CreatedAt)
{
    public static PatientMasterResponse FromEntity(
        PatientMasterEntity patientMaster)
    {
        return new PatientMasterResponse(
            patientMaster.Id ?? string.Empty,
            patientMaster.HisId ?? string.Empty,
            patientMaster.PatientName ?? string.Empty,
            patientMaster.Gender ?? string.Empty,
            patientMaster.CaseId ?? string.Empty,
            patientMaster.Department ?? string.Empty,
            patientMaster.Priority ?? string.Empty,
            patientMaster.SurgeryType ?? string.Empty,
            patientMaster.Status,
            patientMaster.CreatedBy ?? string.Empty,
            patientMaster.CreatedAt);
    }
}