using BookCircle.Application.DTOs;

namespace BookCircle.Application.Services.Interfaces;

public interface IReviewService
{
    Task<IEnumerable<ReviewOutDto>> GetByBookAsync(int clubId, int bookId);
    Task<ReviewOutDto> CreateAsync(ReviewCreateDto dto);
    Task<ReviewOutDto> UpdateAsync(int reviewId, ReviewUpdateDto dto);
    Task DeleteAsync(int reviewId);
}
