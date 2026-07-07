using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.VisitorManagement.Domain.Entities;

public class VisitorGatePass : BaseEntity
{
    [BsonElement("contact_name")]
    public string? ContactName { get; set; }

    [BsonElement("EmailID")]
    public string? EmailId { get; set; }

    [BsonElement("PhoneNo")]
    public string? PhoneNo { get; set; }

    [BsonElement("date_of_visit")]
    public string? DateOfVisit { get; set; }

    [BsonElement("from_date")]
    public DateTime FromDate { get; set; }

    [BsonElement("to_date")]
    public DateTime ToDate { get; set; }

    [BsonElement("reason_of_visit")]
    public string? ReasonOfVisit { get; set; }

    [BsonElement("duration")]
    public string? Duration { get; set; }

    [BsonElement("visiting_time")]
    public string? VisitingTime { get; set; }

    [BsonElement("vehicle_name")]
    public string? VehicleName { get; set; }

    [BsonElement("vehicle_id")]
    public string? VehicleId { get; set; }

    [BsonElement("toolDetails")]
    public List<VisitorGatePassToolDetail> ToolDetails { get; set; } = [];

    [BsonElement("HostCompany")]
    public string? HostCompany { get; set; }

    [BsonElement("VisitorCompany")]
    public string? VisitorCompany { get; set; }

    [BsonElement("HostPerson")]
    public string? HostPerson { get; set; }

    [BsonElement("HostPersonEmail")]
    public string? HostPersonEmail { get; set; }

    [BsonElement("created_by")]
    public string? CreatedBy { get; set; }

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("status")]
    public string? Status { get; set; }

    [BsonElement("VisitorDocuments")]
    public List<VisitorGatePassDocument> VisitorDocuments { get; set; } = [];

    [BsonElement("approved_by")]
    public string? ApprovedBy { get; set; }

    [BsonElement("approved_on")]
    public DateTime? ApprovedOn { get; set; }

    [BsonElement("approved_remarks")]
    public string? ApprovedRemarks { get; set; }

    [BsonElement("visitor_id")]
    public string? VisitorId { get; set; }

    [BsonElement("first_name")]
    public string? FirstName { get; set; }

    [BsonElement("last_name")]
    public string? LastName { get; set; }

    [BsonElement("category_id")]
    public string? CategoryId { get; set; }

    [BsonElement("category")]
    public string? Category { get; set; }

    [BsonElement("mobile_no")]
    public string? MobileNo { get; set; }

    [BsonElement("company_name")]
    public string? CompanyName { get; set; }

    [BsonElement("address")]
    public string? Address { get; set; }

    [BsonElement("company_email")]
    public string? CompanyEmail { get; set; }

    [BsonElement("visitor_pass_referenceno")]
    public string? VisitorPassReferenceNo { get; set; }

    [BsonElement("project_id")]
    public string? ProjectId { get; set; }

    [BsonElement("country_id")]
    public string? CountryId { get; set; }

    [BsonElement("area_id")]
    public string? AreaId { get; set; }

    [BsonElement("building_id")]
    public string? BuildingId { get; set; }

    [BsonElement("FloorId")]
    public string? FloorId { get; set; }

    [BsonElement("ZoneId")]
    public string? ZoneId { get; set; }

    [BsonElement("Zone")]
    public string? Zone { get; set; }

    [BsonElement("isentered")]
    public bool? IsEntered { get; set; }

    [BsonElement("entered_on")]
    public DateTime? EnteredOn { get; set; }

    [BsonElement("isexit")]
    public bool? IsExit { get; set; }

    [BsonElement("exist_on")]
    public DateTime? ExistOn { get; set; }

    [BsonElement("Description")]
    public string? Description { get; set; }

    [BsonElement("authcode")]
    public string? AuthCode { get; set; }

    [BsonElement("IdNo")]
    public string? IdNo { get; set; }

    [BsonElement("IdType")]
    public string? IdType { get; set; }

    [BsonElement("StatusLevel")]
    public int? StatusLevel { get; set; }

    [BsonElement("MaxApprovalLevel")]
    public int? MaxApprovalLevel { get; set; }

    [BsonElement("client_id")]
    public string? ClientId { get; set; }

    [BsonElement("IsLevelProcessed")]
    public bool IsLevelProcessed { get; set; }

    [BsonElement("ProcessedBy")]
    public string? ProcessedBy { get; set; }

    [BsonElement("ProcessedAt")]
    public DateTime? ProcessedAt { get; set; }

    [BsonElement("return_status")]
    public string? ReturnStatus { get; set; }

    [BsonElement("assignAccess")]
    public List<VisitorGatePassAssignAccess> AssignAccess { get; set; } = [];

    [BsonElement("Transactions")]
    public List<VisitorGatePassTransaction> Transactions { get; set; } = [];

    [BsonElement("assignAccessTransaction")]
    public List<VisitorGatePassAssignAccessTransaction> AssignAccessTransaction { get; set; } = [];

    [BsonElement("entrycreatedby")]
    public string? EntryCreatedBy { get; set; }

    [BsonElement("exitcreatedby")]
    public string? ExitCreatedBy { get; set; }

    [BsonElement("returnstatuscreatedby")]
    public string? ReturnStatusCreatedBy { get; set; }

    [BsonElement("ReturnstatusProcessedAt")]
    public DateTime? ReturnStatusProcessedAt { get; set; }

    [BsonElement("VisitorIdNo")]
    public string? VisitorIdNo { get; set; }

    [BsonElement("ApproverChain")]
    public List<string> ApproverChain { get; set; } = [];
}
