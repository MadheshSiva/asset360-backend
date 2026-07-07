using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using A360.Domain.Entities;

namespace A360.People.Domain.Entities;

public class PersonalWorkSchedule : BaseEntity
{
    [BsonElement("work_schedule_name")]
    public string WorkScheduleName { get; set; } = null!;

    [BsonElement("description")]
    public string Description { get; set; } = null!;

    [BsonElement("location")]
    public string Location { get; set; } = null!;

    [BsonElement("groupName")]
    public string GroupName { get; set; } = null!;

    [BsonElement("groupId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string GroupId { get; set; } = null!;

    [BsonElement("status")]
    public bool Status { get; set; }

    [BsonElement("WorkSchedules")]
    public List<WorkScheduleItem> WorkSchedules { get; set; } = [];

    [BsonElement("created_by")]
    public string CreatedBy { get; set; } = null!;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("scheduleType")]
    public string ScheduleType { get; set; } = null!;

    [BsonElement("member")]
    public List<ScheduleMember> Member { get; set; } = [];
}

public class WorkScheduleItem
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

public class ScheduleMember
{
    [BsonElement("memberID")]
    public string MemberID { get; set; } = null!;

    [BsonElement("memberName")]
    public string MemberName { get; set; } = null!;
}