using AutoMapper;
using BookCircle.Application.DTOs;
using BookCircle.Application.Services.Interfaces;
using BookCircle.Domain.Entities;
using BookCircle.Domain.Interfaces;

namespace BookCircle.Application.Services.Implementations;

public class ReviewService(IReviewRepository repo, IMapper mapper) : IReviewService
{
    public async Task<IEnumerable<ReviewOutDto>> GetByBookAsync(int clubId, int bookId)
    {
        var reviews = await repo.GetByBookIdAsync(clubId, bookId);
        return mapper.Map<IEnumerable<ReviewOutDto>>(reviews);
    }

    public async Task<ReviewOutDto> CreateAsync(ReviewCreateDto dto)
    {
        var review = mapper.Map<Review>(dto);
        var created = await repo.CreateAsync(review);
        return mapper.Map<ReviewOutDto>(created);
    }

    public async Task<ReviewOutDto> UpdateAsync(int reviewId, ReviewUpdateDto dto)
    {
        var review = await repo.GetByIdAsync(reviewId)
            ?? throw new KeyNotFoundException($"Review {reviewId} not found.");
        review.Rating = dto.Rating;
        review.Comment = dto.Comment;
        var updated = await repo.UpdateAsync(review);
        return mapper.Map<ReviewOutDto>(updated);
    }

    public async Task DeleteAsync(int reviewId)
    {
        var deleted = await repo.DeleteAsync(reviewId);
        if (!deleted) throw new KeyNotFoundException($"Review {reviewId} not found.");
    }
}
