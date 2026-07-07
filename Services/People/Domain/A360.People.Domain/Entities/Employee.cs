using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.People.Domain.Entities;

public class Employee : BaseEntity
{
    [BsonElement("reference_id")]
    public string ReferenceId { get; set; } = null!;

    [BsonElement("Firstname")]
    public string Firstname { get; set; } = null!;

    [BsonElement("Lastname")]
    public string Lastname { get; set; } = null!;

    [BsonElement("dept")]
    public string Dept { get; set; } = null!;

    [BsonElement("role")]
    public string Role { get; set; } = null!;

    [BsonElement("phone_no")]
    public string PhoneNo { get; set; } = null!;

    [BsonElement("employee_image")]
    public string EmployeeImage { get; set; } = null!;

    [BsonElement("created_by")]
    public string CreatedBy { get; set; } = null!;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("client_id")]
    public string ClientId { get; set; } = null!;

    [BsonElement("Action")]
    public string? Action { get; set; }

    [BsonElement("IDNumber")]
    public string IDNumber { get; set; } = null!;

    [BsonElement("StartDate")]
    public DateTime StartDate { get; set; }

    [BsonElement("EndDate")]
    public DateTime EndDate { get; set; }

    [BsonElement("Company")]
    public string Company { get; set; } = null!;

    [BsonElement("nationalId")]
    public string NationalId { get; set; } = null!;

    [BsonElement("ProjectId")]
    public string ProjecName{get;set;}=null!;

    [BsonElement("SOWId_vehicle_id")]
    public string SOWIdVehicleId { get; set; } = null!;

    [BsonElement("CardBadgeNumber")]
    public string CardBadgeNumber { get; set; } = null!;

    [BsonElement("Variables")]
    public string Variables { get; set; }
}