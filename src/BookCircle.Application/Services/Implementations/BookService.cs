using AutoMapper;
using BookCircle.Application.DTOs;
using BookCircle.Application.Services.Interfaces;
using BookCircle.Domain.Entities;
using BookCircle.Domain.Interfaces;

namespace BookCircle.Application.Services.Implementations;

public class BookService(IBookRepository repo, IMapper mapper) : IBookService
{
    public async Task<IEnumerable<BookOutDto>> GetByClubAsync(int clubId, int skip, int limit)
    {
        var books = await repo.GetByClubIdAsync(clubId, skip, limit);
        return mapper.Map<IEnumerable<BookOutDto>>(books);
    }

    public async Task<BookOutDto> GetByIdAsync(int clubId, int bookId)
    {
        var book = await repo.GetByIdAsync(clubId, bookId)
            ?? throw new KeyNotFoundException($"Book {bookId} not found in club {clubId}.");
        return mapper.Map<BookOutDto>(book);
    }

    public async Task<BookOutDto> CreateAsync(int clubId, BookCreateDto dto)
    {
        var book = mapper.Map<Book>(dto);
        book.ClubId = clubId;
        var created = await repo.CreateAsync(book);
        return mapper.Map<BookOutDto>(created);
    }

    public async Task<object> GetVotesAsync(int clubId, int bookId)
    {
        var book = await repo.GetByIdAsync(clubId, bookId)
            ?? throw new KeyNotFoundException($"Book {bookId} not found.");
        return new { bookId = book.Id, votes = book.Votes };
    }

    public async Task DeleteVotesAsync(int clubId, int bookId)
    {
        var book = await repo.GetByIdAsync(clubId, bookId)
            ?? throw new KeyNotFoundException($"Book {bookId} not found.");
        book.Votes = 0;
        await repo.UpdateAsync(book);
    }

    public async Task<object> GetProgressAsync(int clubId, int bookId)
    {
        var book = await repo.GetByIdAsync(clubId, bookId)
            ?? throw new KeyNotFoundException($"Book {bookId} not found.");
        return new { bookId = book.Id, progress = book.Progress };
    }

    public async Task<BookOutDto> UpdateProgressAsync(int clubId, int bookId, int progress)
    {
        var book = await repo.GetByIdAsync(clubId, bookId)
            ?? throw new KeyNotFoundException($"Book {bookId} not found.");
        book.Progress = progress;
        var updated = await repo.UpdateAsync(book);
        return mapper.Map<BookOutDto>(updated);
    }
}
