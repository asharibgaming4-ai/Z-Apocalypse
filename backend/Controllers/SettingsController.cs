using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectaaa.Data;
using projectaaa.Models;

namespace projectaaa.Controllers;

[ApiController]
[Route("api/settings")]
public class SettingsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SettingsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetSettings(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) return NotFound(new { message = "User not found" });

        return Ok(new
        {
            masterVolume = user.MasterVolume,
            musicVolume = user.MusicVolume,
            sfxVolume = user.SfxVolume,
            graphicsQuality = user.GraphicsQuality,
            fullscreenMode = user.FullscreenMode,
            renderScale = user.RenderScale
        });
    }

    [HttpPost("save")]
    public async Task<IActionResult> SaveSettings([FromBody] SaveSettingsRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
        if (user == null) return NotFound(new { message = "User not found" });

        user.MasterVolume = request.MasterVolume;
        user.MusicVolume = request.MusicVolume;
        user.SfxVolume = request.SfxVolume;
        user.GraphicsQuality = request.GraphicsQuality;
        user.FullscreenMode = request.FullscreenMode;
        user.RenderScale = request.RenderScale;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Settings saved successfully" });
    }
}

public class SaveSettingsRequest
{
    public string Username { get; set; }
    public int MasterVolume { get; set; }
    public int MusicVolume { get; set; }
    public int SfxVolume { get; set; }
    public string GraphicsQuality { get; set; }
    public bool FullscreenMode { get; set; }
    public int RenderScale { get; set; }
}
