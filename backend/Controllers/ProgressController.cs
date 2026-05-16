using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectaaa.Data;
using projectaaa.Models;

namespace projectaaa.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProgressController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProgressController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("maps/{username}")]
    public async Task<IActionResult> GetMaps(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) return NotFound("User not found");

        var maps = await _context.MapLevels.ToListAsync();
        
        var result = maps.Select(m => new {
            m.Id,
            m.MapName,
            m.RequiredLevel,
            m.Difficulty,
            m.ImageUrl,
            IsUnlocked = true // Forced to true for layout testing
        });

        return Ok(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateProgress([FromBody] UserProgressUpdate update)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == update.Username);
        if (user == null) return NotFound("User not found");

        user.Experience += update.ExpGained;
        
        // Simple level up logic: each level requires 1000 XP
        int newLevel = 1 + (user.Experience / 1000);
        if (newLevel > user.Level)
        {
            user.Level = newLevel;
        }

        await _context.SaveChangesAsync();
        return Ok(new { user.Level, user.Experience });
    }
}

public class UserProgressUpdate
{
    public string Username { get; set; }
    public int ExpGained { get; set; }
}
