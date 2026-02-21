using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BookCircle.Application.DTOs;
using BookCircle.Application.Services.Interfaces;
using BookCircle.Domain.Entities;
using BookCircle.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookCircle.Application.Services.Implementations;

public class AuthService(IUserRepository userRepo, IMapper mapper, IConfiguration config) : IAuthService
{
    public async Task<TokenResponse> LoginAsync(string username, string password)
    {
        var user = await userRepo.GetByUsernameAsync(username)
            ?? throw new UnauthorizedAccessException("Invalid credentials.");

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials.");

        var token = GenerateJwt(user);
        return new TokenResponse(token);
    }

    public async Task<UserOutDto> RegisterAsync(UserCreateDto dto)
    {
        var existing = await userRepo.GetByEmailAsync(dto.Email);
        if (existing is not null)
            throw new InvalidOperationException("Email already registered.");

        var user = mapper.Map<User>(dto);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        user.CreatedAt = DateTime.UtcNow;

        var created = await userRepo.CreateAsync(user);
        return mapper.Map<UserOutDto>(created);
    }

    private string GenerateJwt(User user)
    {
        var jwtSettings = config.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpiryMinutes"] ?? "60")),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
