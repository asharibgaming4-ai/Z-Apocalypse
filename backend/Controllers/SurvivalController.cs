using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectaaa.Data;
using projectaaa.Models;

namespace projectaaa.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SurvivalController : ControllerBase
{
    private readonly AppDbContext _context;

    public SurvivalController(AppDbContext context)
    {
        _context = context;
    }

    // POST api/survival/save — Save a survival session after game ends
    [HttpPost("save")]
    public async Task<IActionResult> SaveSession([FromBody] SurvivalSessionRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
        if (user == null) return NotFound(new { message = "User not found" });

        var session = new SurvivalSession
        {
            Username = request.Username,
            MapName = request.MapName ?? "Training Camp",
            SurvivalTimeSeconds = request.SurvivalTimeSeconds,
            Kills = request.Kills,
            CoinsCollected = request.CoinsCollected,
            DiamondsCollected = request.DiamondsCollected,
            Survived = request.Survived,
            PlayedAt = DateTime.UtcNow
        };

        _context.SurvivalSessions.Add(session);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Survival session saved", sessionId = session.Id });
    }

    // GET api/survival/history/{username} — Get survival history for a user
    [HttpGet("history/{username}")]
    public async Task<IActionResult> GetHistory(string username)
    {
        var sessions = await _context.SurvivalSessions
            .Where(s => s.Username == username)
            .OrderByDescending(s => s.PlayedAt)
            .Take(50)
            .ToListAsync();

        return Ok(sessions);
    }

    // GET api/survival/stats/{username} — Get aggregated survival stats
    [HttpGet("stats/{username}")]
    public async Task<IActionResult> GetStats(string username)
    {
        var sessions = await _context.SurvivalSessions
            .Where(s => s.Username == username)
            .ToListAsync();

        if (!sessions.Any())
        {
            return Ok(new
            {
                totalGamesPlayed = 0,
                totalKills = 0,
                totalCoinsCollected = 0,
                totalDiamondsCollected = 0,
                totalSurvivalTime = 0,
                bestSurvivalTime = 0,
                timesVictory = 0,
                timesDefeated = 0
            });
        }

        return Ok(new
        {
            totalGamesPlayed = sessions.Count,
            totalKills = sessions.Sum(s => s.Kills),
            totalCoinsCollected = sessions.Sum(s => s.CoinsCollected),
            totalDiamondsCollected = sessions.Sum(s => s.DiamondsCollected),
            totalSurvivalTime = sessions.Sum(s => s.SurvivalTimeSeconds),
            bestSurvivalTime = sessions.Max(s => s.SurvivalTimeSeconds),
            timesVictory = sessions.Count(s => s.Survived),
            timesDefeated = sessions.Count(s => !s.Survived)
        });
    }
}

public class SurvivalSessionRequest
{
    public string Username { get; set; }
    public string? MapName { get; set; }
    public int SurvivalTimeSeconds { get; set; }
    public int Kills { get; set; }
    public int CoinsCollected { get; set; }
    public int DiamondsCollected { get; set; }
    public bool Survived { get; set; }
}
