using BookCircle.Domain.Entities;

namespace BookCircle.Domain.Interfaces;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetByClubIdAsync(int clubId, int skip, int limit);
    Task<Book?> GetByIdAsync(int clubId, int bookId);
    Task<Book> CreateAsync(Book book);
    Task<Book> UpdateAsync(Book book);
    Task<bool> DeleteAsync(int bookId);
}
