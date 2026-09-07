using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class Manufacturer : BaseEntity
{
    [BsonElement("manufacturer_code")]
    public string ManufacturerCode { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("manufacturer_name")]
    public string ManufacturerName { get; set; } = string.Empty;

    [BsonElement("contact_person")]
    public string ContactPerson { get; set; } = string.Empty;

    [BsonElement("email")]
    public string Email { get; set; } = string.Empty;

    [BsonElement("phone")]
    public string Phone { get; set; } = string.Empty;

    [BsonElement("address")]
    public string Address { get; set; } = string.Empty;

    [BsonElement("website")]
    public string Website { get; set; } = string.Empty;
}
