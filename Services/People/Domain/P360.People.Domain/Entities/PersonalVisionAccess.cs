using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using P360.Domain.Entities;

namespace P360.People.Domain.Entities;

public class PersonalVisionAccess : BaseEntity
{
    [BsonElement("group_name")]
    public string GroupName { get; set; } = null!;

    [BsonElement("group_type")]
    public string GroupType { get; set; } = null!;

    [BsonElement("member")]
    public List<Member> Member { get; set; } = [];

    [BsonElement("reader")]
    public List<Reader> Reader { get; set; } = [];

    [BsonElement("status")]
    public bool Status { get; set; }

    [BsonElement("AccessTimeSchedule")]
    public List<AccessTimeSchedule> AccessTimeSchedule { get; set; } = [];

    [BsonElement("created_by")]
    public string CreatedBy { get; set; } = null!;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("modified_by")]
    public string? ModifiedBy { get; set; }

    [BsonElement("modified_at")]
    public DateTime ModifiedAt { get; set; }

    [BsonElement("Action")]
    public string? Action { get; set; }
}

public class Member
{
    [BsonElement("member_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string MemberId { get; set; } = null!;

    [BsonElement("member_name")]
    public string MemberName { get; set; } = null!;
}

public class Reader
{
    [BsonElement("reader_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ReaderId { get; set; } = null!;

    [BsonElement("reader_name")]
    public string ReaderName { get; set; } = null!;
}

public class AccessTimeSchedule
{
    [BsonElement("from_date")]
    public DateTime FromDate { get; set; }

    [BsonElement("to_date")]
    public DateTime ToDate { get; set; }

    [BsonElement("from_time")]
    public string FromTime { get; set; } = null!;

    [BsonElement("to_time")]
    public string ToTime { get; set; } = null!;
}