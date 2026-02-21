using AutoMapper;
using BookCircle.Application.DTOs;
using BookCircle.Application.Services.Interfaces;
using BookCircle.Domain.Entities;
using BookCircle.Domain.Interfaces;

namespace BookCircle.Application.Services.Implementations;

public class ClubService(IClubRepository repo, IMapper mapper) : IClubService
{
    public async Task<IEnumerable<ClubOutDto>> GetAllAsync(int skip, int limit)
    {
        var clubs = await repo.GetAllAsync(skip, limit);
        return mapper.Map<IEnumerable<ClubOutDto>>(clubs);
    }

    public async Task<ClubOutDto> GetByIdAsync(int id)
    {
        var club = await repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Club {id} not found.");
        return mapper.Map<ClubOutDto>(club);
    }

    public async Task<ClubOutDto> CreateAsync(ClubCreateDto dto)
    {
        var club = mapper.Map<Club>(dto);
        var created = await repo.CreateAsync(club);
        return mapper.Map<ClubOutDto>(created);
    }

    public async Task<ClubOutDto> UpdateAsync(int id, ClubCreateDto dto)
    {
        var club = await repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Club {id} not found.");
        club.Name = dto.Name;
        club.Description = dto.Description;
        club.FavoriteGenre = dto.FavoriteGenre;
        club.Members = dto.Members;
        var updated = await repo.UpdateAsync(club);
        return mapper.Map<ClubOutDto>(updated);
    }

    public async Task DeleteAsync(int id)
    {
        var deleted = await repo.DeleteAsync(id);
        if (!deleted) throw new KeyNotFoundException($"Club {id} not found.");
    }
}
