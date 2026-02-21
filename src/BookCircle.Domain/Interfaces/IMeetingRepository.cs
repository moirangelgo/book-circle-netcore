using BookCircle.Domain.Entities;

namespace BookCircle.Domain.Interfaces;

public interface IMeetingRepository
{
    Task<IEnumerable<Meeting>> GetByClubIdAsync(int clubId);
    Task<Meeting?> GetByIdAsync(int clubId, int meetingId);
    Task<Meeting> CreateAsync(Meeting meeting);
    Task<bool> DeleteAsync(int id);
    Task<MeetingAttendance> AddAttendanceAsync(MeetingAttendance attendance);
}
