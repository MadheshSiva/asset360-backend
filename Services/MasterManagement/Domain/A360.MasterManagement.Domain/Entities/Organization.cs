using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class Organization : BaseEntity
{
    [BsonElement("organization_code")]
    public string OrganizationCode { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("organization_name")]
    public string OrganizationName { get; set; } = string.Empty;

    [BsonElement("legal_name")]
    public string LegalName { get; set; } = string.Empty;

    [BsonElement("logo")]
    public string? Logo { get; set; }

    [BsonElement("address")]
    public string Address { get; set; } = string.Empty;

    [BsonElement("country")]
    public string Country { get; set; } = string.Empty;

    [BsonElement("state")]
    public string State { get; set; } = string.Empty;

    [BsonElement("city")]
    public string City { get; set; } = string.Empty;

    [BsonElement("postal_code")]
    public string PostalCode { get; set; } = string.Empty;

    [BsonElement("contact_person")]
    public string ContactPerson { get; set; } = string.Empty;

    [BsonElement("email")]
    public string Email { get; set; } = string.Empty;

    [BsonElement("phone_number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [BsonElement("time_zone")]
    public string TimeZone { get; set; } = string.Empty;

    [BsonElement("date_format")]
    public string DateFormat { get; set; } = string.Empty;

    [BsonElement("currency")]
    public string Currency { get; set; } = string.Empty;
}
