using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class Site : BaseEntity
{
    [BsonElement("site_code")]
    public string SiteCode { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("site_name")]
    public string SiteName { get; set; } = string.Empty;

    [BsonElement("organization")]
    public string Organization { get; set; } = string.Empty;

    [BsonElement("business_unit")]
    public string BusinessUnit { get; set; } = string.Empty;

    [BsonElement("site_type")]
    public string SiteType { get; set; } = string.Empty;

    [BsonElement("address")]
    public string Address { get; set; } = string.Empty;

    [BsonElement("country")]
    public string Country { get; set; } = string.Empty;

    [BsonElement("state")]
    public string State { get; set; } = string.Empty;

    [BsonElement("city")]
    public string City { get; set; } = string.Empty;

    [BsonElement("gps_latitude")]
    public double? GpsLatitude { get; set; }

    [BsonElement("gps_longitude")]
    public double? GpsLongitude { get; set; }

    [BsonElement("site_manager")]
    public string SiteManager { get; set; } = string.Empty;

    [BsonElement("contact_details")]
    public string ContactDetails { get; set; } = string.Empty;

    [BsonElement("operating_hours")]
    public string OperatingHours { get; set; } = string.Empty;
}
