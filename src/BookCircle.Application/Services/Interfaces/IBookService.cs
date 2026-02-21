using BookCircle.Application.DTOs;

namespace BookCircle.Application.Services.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookOutDto>> GetByClubAsync(int clubId, int skip, int limit);
    Task<BookOutDto> GetByIdAsync(int clubId, int bookId);
    Task<BookOutDto> CreateAsync(int clubId, BookCreateDto dto);
    Task<object> GetVotesAsync(int clubId, int bookId);
    Task DeleteVotesAsync(int clubId, int bookId);
    Task<object> GetProgressAsync(int clubId, int bookId);
    Task<BookOutDto> UpdateProgressAsync(int clubId, int bookId, int progress);
}
