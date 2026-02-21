using BookCircle.Application.DTOs;
using BookCircle.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookCircle.API.Controllers;

[ApiController]
[Route("clubs/{clubId:int}/books")]
[Authorize]
public class BooksController(IBookService bookService) : ControllerBase
{
    /// <summary>Get books by club</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookOutDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByClub(int clubId, [FromQuery] int skip = 0, [FromQuery] int limit = 100)
        => Ok(await bookService.GetByClubAsync(clubId, skip, limit));

    /// <summary>Create a book in a club</summary>
    [HttpPost]
    [ProducesResponseType(typeof(BookOutDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(int clubId, [FromBody] BookCreateDto dto)
    {
        var book = await bookService.CreateAsync(clubId, dto);
        return StatusCode(StatusCodes.Status201Created, book);
    }

    /// <summary>Get a book details</summary>
    [HttpGet("{bookId:int}")]
    [ProducesResponseType(typeof(BookOutDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int clubId, int bookId)
        => Ok(await bookService.GetByIdAsync(clubId, bookId));

    // ── Votes ──────────────────────────────────────────────────────────────────

    /// <summary>Get book votes</summary>
    [HttpGet("{bookId:int}/votes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetVotes(int clubId, int bookId)
        => Ok(await bookService.GetVotesAsync(clubId, bookId));

    /// <summary>Delete / reset book votes</summary>
    [HttpDelete("{bookId:int}/votes")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteVotes(int clubId, int bookId)
    {
        await bookService.DeleteVotesAsync(clubId, bookId);
        return NoContent();
    }

    // ── Progress ───────────────────────────────────────────────────────────────

    /// <summary>Get reading progress</summary>
    [HttpGet("{bookId:int}/progress")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProgress(int clubId, int bookId)
        => Ok(await bookService.GetProgressAsync(clubId, bookId));

    /// <summary>Update reading progress</summary>
    [HttpPut("{bookId:int}/progress")]
    [ProducesResponseType(typeof(BookOutDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateProgress(int clubId, int bookId, [FromQuery] int progress)
        => Ok(await bookService.UpdateProgressAsync(clubId, bookId, progress));
}
