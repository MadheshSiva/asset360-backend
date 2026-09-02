using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.MasterManagement.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class CurrentLocation : BaseEntity
{
    [BsonElement("location_id")]
    public string LocationId { get; set; } = string.Empty;

    [BsonElement("current_location")]
    public string CurrentLocationName { get; set; } = string.Empty;

    [BsonElement("active")]
    public bool Active { get; set; }
}
