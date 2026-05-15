using Microsoft.AspNetCore.Mvc;
using projectaaa.Services;

namespace projectaaa.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _authService.Authenticate(request.Username, request.Password);
        if (user == null) return Unauthorized(new { message = "Invalid credentials" });

        return Ok(new { username = user.Username, email = user.Email });
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupRequest request)
    {
        var user = await _authService.Register(request.Username, request.Email, request.Password);
        if (user == null) return BadRequest(new { message = "Username already taken" });

        return Ok(new { username = user.Username, message = "User created successfully" });
    }
}

public class LoginRequest { public string Username { get; set; } public string Password { get; set; } }
public class SignupRequest { public string Username { get; set; } public string Email { get; set; } public string Password { get; set; } }
