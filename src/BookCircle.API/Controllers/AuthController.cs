using BookCircle.Application.DTOs;
using BookCircle.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookCircle.API.Controllers;

[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    /// <summary>Login for access token</summary>
    [HttpPost("/token")]
    [Consumes("application/x-www-form-urlencoded")]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Token([FromForm] string username, [FromForm] string password)
    {
        var token = await authService.LoginAsync(username, password);
        return Ok(token);
    }

    /// <summary>Register a new user</summary>
    [HttpPost("/auth/register")]
    [ProducesResponseType(typeof(UserOutDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromBody] UserCreateDto dto)
    {
        var user = await authService.RegisterAsync(dto);
        return StatusCode(StatusCodes.Status201Created, user);
    }
}
