using BookCircle.Application.DTOs;
using BookCircle.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookCircle.API.Controllers;

[ApiController]
[Route("clubs/{clubId:int}/meetings")]
[Authorize]
public class MeetingsController(IMeetingService meetingService) : ControllerBase
{
    /// <summary>List meetings for a club</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MeetingOutDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByClub(int clubId)
        => Ok(await meetingService.GetByClubAsync(clubId));

    /// <summary>Create a meeting</summary>
    [HttpPost]
    [ProducesResponseType(typeof(MeetingOutDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(int clubId, [FromBody] MeetingCreateDto dto)
    {
        var meeting = await meetingService.CreateAsync(clubId, dto);
        return StatusCode(StatusCodes.Status201Created, meeting);
    }

    /// <summary>Get a meeting by ID</summary>
    [HttpGet("{meetingId:int}")]
    [ProducesResponseType(typeof(MeetingOutDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int clubId, int meetingId)
        => Ok(await meetingService.GetByIdAsync(clubId, meetingId));

    /// <summary>Cancel / delete a meeting</summary>
    [HttpDelete("{meetingId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int clubId, int meetingId)
    {
        await meetingService.DeleteAsync(clubId, meetingId);
        return NoContent();
    }

    /// <summary>Confirm attendance for a meeting</summary>
    [HttpPost("{meetingId:int}/attendance")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> ConfirmAttendance(int clubId, int meetingId, [FromBody] MeetingAttendanceCreateDto dto)
    {
        await meetingService.ConfirmAttendanceAsync(clubId, meetingId, dto);
        return StatusCode(StatusCodes.Status201Created);
    }
}
