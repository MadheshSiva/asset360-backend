using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.People.Domain.Entities;

public class PersonalVisionManualAttendance : BaseEntity
{
    [BsonElement("employee_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string EmployeeId { get; set; } = null!;

    [BsonElement("employee_name")]
    public string? EmployeeName { get; set; }

    [BsonElement("reason")]
    public string? Reason { get; set; }

    [BsonElement("from_date")]
    public DateTime FromDate { get; set; }

    [BsonElement("from_time")]
    public string? FromTime { get; set; }

    [BsonElement("attendence_status")]
    public string? AttendanceStatus { get; set; }

    [BsonElement("approve_status")]
    public string? ApproveStatus { get; set; }

    [BsonElement("approved_by")]
    public string? ApprovedBy { get; set; }

    [BsonElement("approved_on")]
    public DateTime? ApprovedOn { get; set; }

    [BsonElement("approved_remarks")]
    public string? ApprovedRemarks { get; set; }

    [BsonElement("Action")]
    public string? Action { get; set; }
}