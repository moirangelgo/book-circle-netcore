using AutoMapper;
using BookCircle.Application.DTOs;
using BookCircle.Application.Services.Interfaces;
using BookCircle.Domain.Entities;
using BookCircle.Domain.Interfaces;

namespace BookCircle.Application.Services.Implementations;

public class MeetingService(IMeetingRepository repo, IMapper mapper) : IMeetingService
{
    public async Task<IEnumerable<MeetingOutDto>> GetByClubAsync(int clubId)
    {
        var meetings = await repo.GetByClubIdAsync(clubId);
        return mapper.Map<IEnumerable<MeetingOutDto>>(meetings);
    }

    public async Task<MeetingOutDto> GetByIdAsync(int clubId, int meetingId)
    {
        var meeting = await repo.GetByIdAsync(clubId, meetingId)
            ?? throw new KeyNotFoundException($"Meeting {meetingId} not found.");
        return mapper.Map<MeetingOutDto>(meeting);
    }

    public async Task<MeetingOutDto> CreateAsync(int clubId, MeetingCreateDto dto)
    {
        var meeting = mapper.Map<Meeting>(dto);
        meeting.ClubId = clubId;
        var created = await repo.CreateAsync(meeting);
        return mapper.Map<MeetingOutDto>(created);
    }

    public async Task DeleteAsync(int clubId, int meetingId)
    {
        var deleted = await repo.DeleteAsync(meetingId);
        if (!deleted) throw new KeyNotFoundException($"Meeting {meetingId} not found.");
    }

    public async Task ConfirmAttendanceAsync(int clubId, int meetingId, MeetingAttendanceCreateDto dto)
    {
        var attendance = mapper.Map<MeetingAttendance>(dto);
        attendance.MeetingId = meetingId;
        await repo.AddAttendanceAsync(attendance);
    }
}
