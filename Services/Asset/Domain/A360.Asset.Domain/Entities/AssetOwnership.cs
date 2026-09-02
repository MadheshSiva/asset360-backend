using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.Asset.Domain.Entities;

[BsonIgnoreExtraElements]
public sealed class AssetOwnership : BaseEntity
{
    [BsonElement("ownership_id")]
    public string OwnershipId { get; set; } = string.Empty;

    [BsonElement("asset_id")]
    public string AssetId { get; set; } = string.Empty;

    [BsonElement("asset_name")]
    public string AssetName { get; set; } = string.Empty;

    [BsonElement("assigned_custodian")]
    public string AssignedCustodian { get; set; } = string.Empty;

    [BsonElement("department")]
    public string Department { get; set; } = string.Empty;

    [BsonElement("assignment_start_date")]
    public DateTime? AssignmentStartDate { get; set; }

    [BsonElement("assignment_end_date")]
    public DateTime? AssignmentEndDate { get; set; }

    [BsonElement("transfer_history")]
    public string TransferHistory { get; set; } = string.Empty;

    [BsonElement("custodian_details")]
    public string CustodianDetails { get; set; } = string.Empty;

    [BsonElement("check_in_out_logs")]
    public string CheckInOutLogs { get; set; } = string.Empty;
}
