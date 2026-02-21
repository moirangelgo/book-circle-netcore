using BookCircle.Application.DTOs;
using BookCircle.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookCircle.API.Controllers;

[ApiController]
[Route("clubs")]
[Authorize]
public class ClubsController(IClubService clubService) : ControllerBase
{
    /// <summary>List all clubs</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ClubOutDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int skip = 0, [FromQuery] int limit = 100)
        => Ok(await clubService.GetAllAsync(skip, limit));

    /// <summary>Create a club</summary>
    [HttpPost]
    [ProducesResponseType(typeof(ClubOutDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] ClubCreateDto dto)
    {
        var club = await clubService.CreateAsync(dto);
        return StatusCode(StatusCodes.Status201Created, club);
    }

    /// <summary>Get a club by ID</summary>
    [HttpGet("{clubId:int}")]
    [ProducesResponseType(typeof(ClubOutDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int clubId)
        => Ok(await clubService.GetByIdAsync(clubId));

    /// <summary>Update a club</summary>
    [HttpPut("{clubId:int}")]
    [ProducesResponseType(typeof(ClubOutDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int clubId, [FromBody] ClubCreateDto dto)
        => Ok(await clubService.UpdateAsync(clubId, dto));

    /// <summary>Delete a club</summary>
    [HttpDelete("{clubId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int clubId)
    {
        await clubService.DeleteAsync(clubId);
        return NoContent();
    }
}
