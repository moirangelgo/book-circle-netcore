using BookCircle.Domain.Enums;

namespace BookCircle.Domain.Entities;

public class MeetingAttendance
{
    public int Id { get; set; }
    public int MeetingId { get; set; }
    public int UserId { get; set; }
    public AttendanceValue Status { get; set; }

    public Meeting Meeting { get; set; } = null!;
}
