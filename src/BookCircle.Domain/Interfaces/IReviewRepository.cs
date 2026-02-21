using BookCircle.Domain.Entities;

namespace BookCircle.Domain.Interfaces;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetByBookIdAsync(int clubId, int bookId);
    Task<Review?> GetByIdAsync(int id);
    Task<Review> CreateAsync(Review review);
    Task<Review> UpdateAsync(Review review);
    Task<bool> DeleteAsync(int id);
}
