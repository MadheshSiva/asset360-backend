using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.People.Domain.Entities;

public class Visitor : BaseEntity
{
    [BsonElement("reference_id")]
    public string ReferenceId { get; set; } = null!;

    [BsonElement("phone_no")]
    public string PhoneNo { get; set; } = null!;

    [BsonElement("Firstname")]
    public string Firstname { get; set; } = null!;

    [BsonElement("Lastname")]
    public string Lastname { get; set; } = null!;

    [BsonElement("dept")]
    public string Dept { get; set; } = null!;

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

    [BsonElement("SOWId_vehicle_id")]
    public string SOWIdVehicleId { get; set; } = null!;

    [BsonElement("CardBadgeNumber")]
    public string CardBadgeNumber { get; set; } = null!;

    [BsonElement("visitor_image")]
    public string VisitorImage { get; set; } = null!;

    [BsonElement("email")]
    public string Email { get; set; } = null!;

    [BsonElement("authcode")]
    public string AuthCode { get; set; } = null!;

    [BsonElement("Documenttype")]
    public string DocumentType { get; set; } = null!;

    [BsonElement("Documentid")]
    public string DocumentId { get; set; } = null!;

    [BsonElement("visitorcompany")]
    public string VisitorCompany { get; set; } = null!;

    [BsonElement("Action")]
    public string? Action { get; set; }

    [BsonElement("hostPerson")]
    public string HostPerson { get; set; } = null!;

    [BsonElement("hostPersonEmail")]
    public string HostPersonEmail { get; set; } = null!;
}