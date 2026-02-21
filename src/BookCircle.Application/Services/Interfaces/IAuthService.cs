using BookCircle.Application.DTOs;

namespace BookCircle.Application.Services.Interfaces;

public interface IAuthService
{
    Task<TokenResponse> LoginAsync(string username, string password);
    Task<UserOutDto> RegisterAsync(UserCreateDto dto);
}
