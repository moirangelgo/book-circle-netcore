using BookCircle.Application.DTOs;

namespace BookCircle.Application.Services.Interfaces;

public interface IMeetingService
{
    Task<IEnumerable<MeetingOutDto>> GetByClubAsync(int clubId);
    Task<MeetingOutDto> GetByIdAsync(int clubId, int meetingId);
    Task<MeetingOutDto> CreateAsync(int clubId, MeetingCreateDto dto);
    Task DeleteAsync(int clubId, int meetingId);
    Task ConfirmAttendanceAsync(int clubId, int meetingId, MeetingAttendanceCreateDto dto);
}
