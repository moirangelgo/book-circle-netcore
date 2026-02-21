namespace BookCircle.Domain.Entities;

public class Meeting
{
    public int Id { get; set; }
    public int ClubId { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public string? ScheduledAt { get; set; }
    public int? Duration { get; set; }
    public string Location { get; set; } = string.Empty;
    public string LocationUrl { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? CreatedBy { get; set; }
    public int? AttendeeCount { get; set; }
    public string? Status { get; set; }
    public bool? IsVirtual { get; set; }
    public string VirtualMeetingUrl { get; set; } = string.Empty;

    public Club Club { get; set; } = null!;
    public ICollection<MeetingAttendance> Attendances { get; set; } = new List<MeetingAttendance>();
}
