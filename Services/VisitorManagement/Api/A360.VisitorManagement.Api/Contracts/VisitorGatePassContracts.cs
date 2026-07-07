using GatePassEntity = A360.VisitorManagement.Domain.Entities.VisitorGatePass;
using A360.VisitorManagement.Domain.Entities;

namespace A360.VisitorManagement.Api.Contracts;

public sealed record GatePassToolDetailDto(
    string? ToolsName,
    string? ToolsQuantity,
    string? Returnable,
    string? Remarks,
    string? SerialNo,
    string? ToolStatus,
    string? ToolEmail,
    DateTime? ModifiedAt,
    string? ToolUniqueId,
    bool IsClosedEnabled,
    bool IsMainRow)
{
    public VisitorGatePassToolDetail ToEntity()
    {
        return new VisitorGatePassToolDetail
        {
            ToolsName = ToolsName,
            ToolsQuantity = ToolsQuantity,
            Returnable = Returnable,
            Remarks = Remarks,
            SerialNo = SerialNo,
            ToolStatus = ToolStatus,
            ToolEmail = ToolEmail,
            ModifiedAt = ModifiedAt,
            ToolUniqueId = ToolUniqueId,
            IsClosedEnabled = IsClosedEnabled,
            IsMainRow = IsMainRow
        };
    }

    public static GatePassToolDetailDto FromEntity(VisitorGatePassToolDetail entity)
    {
        return new GatePassToolDetailDto(
            entity.ToolsName ?? string.Empty,
            entity.ToolsQuantity ?? string.Empty,
            entity.Returnable ?? string.Empty,
            entity.Remarks ?? string.Empty,
            entity.SerialNo ?? string.Empty,
            entity.ToolStatus ?? string.Empty,
            entity.ToolEmail ?? string.Empty,
            entity.ModifiedAt,
            entity.ToolUniqueId ?? string.Empty,
            entity.IsClosedEnabled,
            entity.IsMainRow);
    }
}

public sealed record GatePassDocumentDto(
    string? DocType,
    string? DocNumber,
    string? ExpiresOn,
    string? Upload)
{
    public VisitorGatePassDocument ToEntity()
    {
        return new VisitorGatePassDocument
        {
            DocType = DocType,
            DocNumber = DocNumber,
            ExpiresOn = ExpiresOn,
            Upload = Upload
        };
    }

    public static GatePassDocumentDto FromEntity(VisitorGatePassDocument entity)
    {
        return new GatePassDocumentDto(
            entity.DocType ?? string.Empty,
            entity.DocNumber ?? string.Empty,
            entity.ExpiresOn ?? string.Empty,
            entity.Upload ?? string.Empty);
    }
}

public sealed record GatePassAssignAccessDto(
    string? AccessName,
    string? AccessId)
{
    public VisitorGatePassAssignAccess ToEntity()
    {
        return new VisitorGatePassAssignAccess
        {
            AccessName = AccessName,
            AccessId = AccessId
        };
    }

    public static GatePassAssignAccessDto FromEntity(VisitorGatePassAssignAccess entity)
    {
        return new GatePassAssignAccessDto(
            entity.AccessName ?? string.Empty,
            entity.AccessId ?? string.Empty);
    }
}

public sealed record GatePassTransactionDto(
    string? Description,
    string? LevelStatus,
    string? CreatedBy,
    DateTime CreatedOn)
{
    public static GatePassTransactionDto FromEntity(VisitorGatePassTransaction entity)
    {
        return new GatePassTransactionDto(
            entity.Description ?? string.Empty,
            entity.LevelStatus ?? string.Empty,
            entity.CreatedBy ?? string.Empty,
            entity.CreatedOn);
    }
}

public sealed record GatePassAssignAccessTransactionDto(
    string? Action,
    string? AccessName,
    string? CreatedBy,
    DateTime CreatedOn)
{
    public VisitorGatePassAssignAccessTransaction ToEntity()
    {
        return new VisitorGatePassAssignAccessTransaction
        {
            Action = Action,
            AccessName = AccessName,
            CreatedBy = CreatedBy,
            CreatedOn = CreatedOn
        };
    }

    public static GatePassAssignAccessTransactionDto FromEntity(VisitorGatePassAssignAccessTransaction entity)
    {
        return new GatePassAssignAccessTransactionDto(
            entity.Action ?? string.Empty,
            entity.AccessName ?? string.Empty,
            entity.CreatedBy ?? string.Empty,
            entity.CreatedOn);
    }
}

public sealed record CreateVisitorGatePassRequest(
    string? ContactName,
    string? EmailId,
    string? PhoneNo,
    string? DateOfVisit,
    DateTime FromDate,
    DateTime ToDate,
    string? ReasonOfVisit,
    string? Duration,
    string? VisitingTime,
    string? VehicleName,
    string? VehicleId,
    List<GatePassToolDetailDto>? ToolDetails,
    string? HostCompany,
    string? VisitorCompany,
    string? HostPerson,
    string? HostPersonEmail,
    string? CreatedBy,
    List<GatePassDocumentDto>? VisitorDocuments,
    string? VisitorId,
    string? FirstName,
    string? LastName,
    string? CategoryId,
    string? Category,
    string? MobileNo,
    string? CompanyName,
    string? Address,
    string? CompanyEmail,
    string? ProjectId,
    string? CountryId,
    string? AreaId,
    string? BuildingId,
    string? FloorId,
    string? ZoneId,
    string? Zone,
    string? Description,
    string? IdNo,
    string? IdType,
    string? ClientId,
    List<GatePassAssignAccessDto>? AssignAccess,
    string? VisitorIdNo)
{
    public GatePassEntity ToEntity()
    {
        return new GatePassEntity
        {
            ContactName = ContactName,
            EmailId = EmailId,
            PhoneNo = PhoneNo,
            DateOfVisit = DateOfVisit,
            FromDate = FromDate,
            ToDate = ToDate,
            ReasonOfVisit = ReasonOfVisit,
            Duration = Duration,
            VisitingTime = VisitingTime,
            VehicleName = VehicleName,
            VehicleId = VehicleId,
            ToolDetails = ToolDetails?.Select(x => x.ToEntity()).ToList() ?? [],
            HostCompany = HostCompany,
            VisitorCompany = VisitorCompany,
            HostPerson = HostPerson,
            HostPersonEmail = HostPersonEmail,
            CreatedBy = CreatedBy,
            VisitorDocuments = VisitorDocuments?.Select(x => x.ToEntity()).ToList() ?? [],
            VisitorId = VisitorId,
            FirstName = FirstName,
            LastName = LastName,
            CategoryId = CategoryId,
            Category = Category,
            MobileNo = MobileNo,
            CompanyName = CompanyName,
            Address = Address,
            CompanyEmail = CompanyEmail,
            ProjectId = ProjectId,
            CountryId = CountryId,
            AreaId = AreaId,
            BuildingId = BuildingId,
            FloorId = FloorId,
            ZoneId = ZoneId,
            Zone = Zone,
            Description = Description,
            IdNo = IdNo,
            IdType = IdType,
            ClientId = ClientId,
            AssignAccess = AssignAccess?.Select(x => x.ToEntity()).ToList() ?? [],
            VisitorIdNo = VisitorIdNo,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public sealed record UpdateVisitorGatePassRequest(
    string? ContactName,
    string? EmailId,
    string? PhoneNo,
    string? DateOfVisit,
    DateTime FromDate,
    DateTime ToDate,
    string? ReasonOfVisit,
    string? Duration,
    string? VisitingTime,
    string? VehicleName,
    string? VehicleId,
    List<GatePassToolDetailDto>? ToolDetails,
    string? HostCompany,
    string? VisitorCompany,
    string? HostPerson,
    string? HostPersonEmail,
    List<GatePassDocumentDto>? VisitorDocuments,
    string? VisitorId,
    string? FirstName,
    string? LastName,
    string? CategoryId,
    string? Category,
    string? MobileNo,
    string? CompanyName,
    string? Address,
    string? CompanyEmail,
    string? ProjectId,
    string? CountryId,
    string? AreaId,
    string? BuildingId,
    string? FloorId,
    string? ZoneId,
    string? Zone,
    string? Description,
    string? IdNo,
    string? IdType,
    string? ClientId,
    List<GatePassAssignAccessDto>? AssignAccess,
    bool? IsEntered,
    DateTime? EnteredOn,
    bool? IsExit,
    DateTime? ExistOn,
    string? ReturnStatus,
    string? EntryCreatedBy,
    string? ExitCreatedBy,
    string? ReturnStatusCreatedBy,
    string? VisitorIdNo)
{
    public void ApplyTo(GatePassEntity gatePass)
    {
        gatePass.ContactName = ContactName;
        gatePass.EmailId = EmailId;
        gatePass.PhoneNo = PhoneNo;
        gatePass.DateOfVisit = DateOfVisit;
        gatePass.FromDate = FromDate;
        gatePass.ToDate = ToDate;
        gatePass.ReasonOfVisit = ReasonOfVisit;
        gatePass.Duration = Duration;
        gatePass.VisitingTime = VisitingTime;
        gatePass.VehicleName = VehicleName;
        gatePass.VehicleId = VehicleId;
        gatePass.ToolDetails = ToolDetails?.Select(x => x.ToEntity()).ToList() ?? [];
        gatePass.HostCompany = HostCompany;
        gatePass.VisitorCompany = VisitorCompany;
        gatePass.HostPerson = HostPerson;
        gatePass.HostPersonEmail = HostPersonEmail;
        gatePass.VisitorDocuments = VisitorDocuments?.Select(x => x.ToEntity()).ToList() ?? [];
        gatePass.VisitorId = VisitorId;
        gatePass.FirstName = FirstName;
        gatePass.LastName = LastName;
        gatePass.CategoryId = CategoryId;
        gatePass.Category = Category;
        gatePass.MobileNo = MobileNo;
        gatePass.CompanyName = CompanyName;
        gatePass.Address = Address;
        gatePass.CompanyEmail = CompanyEmail;
        gatePass.ProjectId = ProjectId;
        gatePass.CountryId = CountryId;
        gatePass.AreaId = AreaId;
        gatePass.BuildingId = BuildingId;
        gatePass.FloorId = FloorId;
        gatePass.ZoneId = ZoneId;
        gatePass.Zone = Zone;
        gatePass.Description = Description;
        gatePass.IdNo = IdNo;
        gatePass.IdType = IdType;
        gatePass.ClientId = ClientId;
        gatePass.AssignAccess = AssignAccess?.Select(x => x.ToEntity()).ToList() ?? [];
        gatePass.IsEntered = IsEntered;
        gatePass.EnteredOn = EnteredOn;
        gatePass.IsExit = IsExit;
        gatePass.ExistOn = ExistOn;
        gatePass.ReturnStatus = ReturnStatus;
        gatePass.EntryCreatedBy = EntryCreatedBy;
        gatePass.ExitCreatedBy = ExitCreatedBy;
        gatePass.ReturnStatusCreatedBy = ReturnStatusCreatedBy;
        gatePass.VisitorIdNo = VisitorIdNo;
    }
}

public sealed record ApproveGatePassRequest(
    string? ApproverEmail,
    string? Remarks);

public sealed record RejectGatePassRequest(
    string? ApproverEmail,
    string? Remarks);

public sealed record VisitorGatePassResponse(
    string Id,
    string ContactName,
    string EmailId,
    string PhoneNo,
    string DateOfVisit,
    DateTime FromDate,
    DateTime ToDate,
    string ReasonOfVisit,
    string Duration,
    string VisitingTime,
    string VehicleName,
    string VehicleId,
    List<GatePassToolDetailDto> ToolDetails,
    string HostCompany,
    string VisitorCompany,
    string HostPerson,
    string HostPersonEmail,
    string CreatedBy,
    DateTime CreatedAt,
    string Status,
    List<GatePassDocumentDto> VisitorDocuments,
    string ApprovedBy,
    DateTime? ApprovedOn,
    string ApprovedRemarks,
    string VisitorId,
    string FirstName,
    string LastName,
    string CategoryId,
    string Category,
    string MobileNo,
    string CompanyName,
    string Address,
    string CompanyEmail,
    string VisitorPassReferenceNo,
    string ProjectId,
    string CountryId,
    string AreaId,
    string BuildingId,
    string FloorId,
    string ZoneId,
    string Zone,
    bool? IsEntered,
    DateTime? EnteredOn,
    bool? IsExit,
    DateTime? ExistOn,
    string Description,
    string AuthCode,
    string IdNo,
    string IdType,
    int? StatusLevel,
    int? MaxApprovalLevel,
    string ClientId,
    bool IsLevelProcessed,
    string ProcessedBy,
    DateTime? ProcessedAt,
    string ReturnStatus,
    List<GatePassAssignAccessDto> AssignAccess,
    List<GatePassTransactionDto> Transactions,
    List<GatePassAssignAccessTransactionDto> AssignAccessTransaction,
    string EntryCreatedBy,
    string ExitCreatedBy,
    string ReturnStatusCreatedBy,
    DateTime? ReturnStatusProcessedAt,
    string VisitorIdNo,
    List<string> ApproverChain)
{
    public static VisitorGatePassResponse FromEntity(GatePassEntity gatePass)
    {
        return new VisitorGatePassResponse(
            gatePass.Id ?? string.Empty,
            gatePass.ContactName ?? string.Empty,
            gatePass.EmailId ?? string.Empty,
            gatePass.PhoneNo ?? string.Empty,
            gatePass.DateOfVisit ?? string.Empty,
            gatePass.FromDate,
            gatePass.ToDate,
            gatePass.ReasonOfVisit ?? string.Empty,
            gatePass.Duration ?? string.Empty,
            gatePass.VisitingTime ?? string.Empty,
            gatePass.VehicleName ?? string.Empty,
            gatePass.VehicleId ?? string.Empty,
            gatePass.ToolDetails.Select(GatePassToolDetailDto.FromEntity).ToList(),
            gatePass.HostCompany ?? string.Empty,
            gatePass.VisitorCompany ?? string.Empty,
            gatePass.HostPerson ?? string.Empty,
            gatePass.HostPersonEmail ?? string.Empty,
            gatePass.CreatedBy ?? string.Empty,
            gatePass.CreatedAt,
            gatePass.Status ?? string.Empty,
            gatePass.VisitorDocuments.Select(GatePassDocumentDto.FromEntity).ToList(),
            gatePass.ApprovedBy ?? string.Empty,
            gatePass.ApprovedOn,
            gatePass.ApprovedRemarks ?? string.Empty,
            gatePass.VisitorId ?? string.Empty,
            gatePass.FirstName ?? string.Empty,
            gatePass.LastName ?? string.Empty,
            gatePass.CategoryId ?? string.Empty,
            gatePass.Category ?? string.Empty,
            gatePass.MobileNo ?? string.Empty,
            gatePass.CompanyName ?? string.Empty,
            gatePass.Address ?? string.Empty,
            gatePass.CompanyEmail ?? string.Empty,
            gatePass.VisitorPassReferenceNo ?? string.Empty,
            gatePass.ProjectId ?? string.Empty,
            gatePass.CountryId ?? string.Empty,
            gatePass.AreaId ?? string.Empty,
            gatePass.BuildingId ?? string.Empty,
            gatePass.FloorId ?? string.Empty,
            gatePass.ZoneId ?? string.Empty,
            gatePass.Zone ?? string.Empty,
            gatePass.IsEntered,
            gatePass.EnteredOn,
            gatePass.IsExit,
            gatePass.ExistOn,
            gatePass.Description ?? string.Empty,
            gatePass.AuthCode ?? string.Empty,
            gatePass.IdNo ?? string.Empty,
            gatePass.IdType ?? string.Empty,
            gatePass.StatusLevel,
            gatePass.MaxApprovalLevel,
            gatePass.ClientId ?? string.Empty,
            gatePass.IsLevelProcessed,
            gatePass.ProcessedBy ?? string.Empty,
            gatePass.ProcessedAt,
            gatePass.ReturnStatus ?? string.Empty,
            gatePass.AssignAccess.Select(GatePassAssignAccessDto.FromEntity).ToList(),
            gatePass.Transactions.Select(GatePassTransactionDto.FromEntity).ToList(),
            gatePass.AssignAccessTransaction.Select(GatePassAssignAccessTransactionDto.FromEntity).ToList(),
            gatePass.EntryCreatedBy ?? string.Empty,
            gatePass.ExitCreatedBy ?? string.Empty,
            gatePass.ReturnStatusCreatedBy ?? string.Empty,
            gatePass.ReturnStatusProcessedAt,
            gatePass.VisitorIdNo ?? string.Empty,
            gatePass.ApproverChain);
    }
}
