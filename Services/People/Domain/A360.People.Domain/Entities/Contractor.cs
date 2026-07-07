using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.People.Domain.Entities;

public class Contractor : BaseEntity
{
    [BsonElement("reference_id")]
    public string ReferenceId { get; set; } = null!;

    [BsonElement("contractor_name")]
    public string ContractorName { get; set; } = null!;

    [BsonElement("contractor_id")]
    public string ContractorId { get; set; } = null!;

    [BsonElement("companyName")]
    public string CompanyName { get; set; } = null!;

    [BsonElement("project_name")]
    public string ProjectName { get; set; } = null!;

    [BsonElement("address")]
    public string Address { get; set; } = null!;

    [BsonElement("contract_start")]
    public DateTime ContractStart { get; set; }

    [BsonElement("contract_end")]
    public DateTime ContractEnd { get; set; }

    [BsonElement("phone_no")]
    public string PhoneNo { get; set; } = null!;

    [BsonElement("nationality")]
    public string Nationality { get; set; } = null!;

    [BsonElement("vehicle_name")]
    public string VehicleName { get; set; } = null!;

    [BsonElement("vehicle_id")]
    public string VehicleId { get; set; } = null!;

    [BsonElement("contractor_image")]
    public string? ContractorImage { get; set; }

    [BsonElement("created_by")]
    public string CreatedBy { get; set; } = null!;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("client_id")]
    public string ClientId { get; set; } = null!;
}