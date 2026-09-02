using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.VisitorManagement.Domain.Entities;

public class VisitorPanelSetting : BaseEntity
{
    [BsonElement("background_img")]
    public string? BackgroundImg { get; set; }

    [BsonElement("logo")]
    public string? Logo { get; set; }

    [BsonElement("CompanyName")]
    public string? CompanyName { get; set; }

    [BsonElement("isauthcode")]
    public bool IsAuthCode { get; set; }

    [BsonElement("isapproved")]
    public bool IsApproved { get; set; }

    [BsonElement("visitorpanelname")]
    public string? VisitorPanelName { get; set; }
}
