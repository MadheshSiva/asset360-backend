using MongoDB.Bson.Serialization.Attributes;

namespace A360.VisitorManagement.Domain.Entities;

public class VisitorGatePassToolDetail
{
    [BsonElement("tools_name")]
    public string? ToolsName { get; set; }

    [BsonElement("tools_quantity")]
    public string? ToolsQuantity { get; set; }

    [BsonElement("Returnable")]
    public string? Returnable { get; set; }

    [BsonElement("Remarks")]
    public string? Remarks { get; set; }

    [BsonElement("serial_no")]
    public string? SerialNo { get; set; }

    [BsonElement("tool_status")]
    public string? ToolStatus { get; set; }

    [BsonElement("tool_email")]
    public string? ToolEmail { get; set; }

    [BsonElement("modified_at")]
    public DateTime? ModifiedAt { get; set; }

    [BsonElement("ToolUniqueId")]
    public string? ToolUniqueId { get; set; }

    [BsonElement("isclosedenabled")]
    public bool IsClosedEnabled { get; set; }

    [BsonElement("isMainRow")]
    public bool IsMainRow { get; set; }
}
