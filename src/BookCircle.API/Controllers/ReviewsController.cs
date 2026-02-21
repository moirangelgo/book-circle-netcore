using BookCircle.Application.DTOs;
using BookCircle.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookCircle.API.Controllers;

[ApiController]
[Route("clubs/{clubId:int}/books/{bookId:int}/reviews")]
[Authorize]
public class ReviewsController(IReviewService reviewService) : ControllerBase
{
    /// <summary>Get reviews by book</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ReviewOutDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByBook(int clubId, int bookId)
        => Ok(await reviewService.GetByBookAsync(clubId, bookId));

    /// <summary>Create a review</summary>
    [HttpPost]
    [ProducesResponseType(typeof(ReviewOutDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(int clubId, int bookId, [FromBody] ReviewCreateDto dto)
    {
        var review = await reviewService.CreateAsync(dto);
        return StatusCode(StatusCodes.Status201Created, review);
    }

    /// <summary>Update a review</summary>
    [HttpPut("{reviewId:int}")]
    [ProducesResponseType(typeof(ReviewOutDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int clubId, int bookId, int reviewId, [FromBody] ReviewUpdateDto dto)
        => Ok(await reviewService.UpdateAsync(reviewId, dto));

    /// <summary>Delete a review</summary>
    [HttpDelete("{reviewId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int clubId, int bookId, int reviewId)
    {
        await reviewService.DeleteAsync(reviewId);
        return NoContent();
    }
}
