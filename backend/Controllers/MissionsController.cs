using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectaaa.Data;
using projectaaa.Models;

namespace projectaaa.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MissionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public MissionsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetMissions(string username)
    {
        var missions = await _context.UserMissions
            .Where(m => m.Username == username)
            .ToListAsync();
            
        if (!missions.Any())
        {
            // Auto-initialize default missions for new users
            var defaults = new List<UserMission>
            {
                new UserMission { Username = username, Title = "15 HEADSHOTS", CurrentProgress = 12, Goal = 15 },
                new UserMission { Username = username, Title = "SURVIVE Z-WAVE", CurrentProgress = 0, Goal = 1 },
                new UserMission { Username = username, Title = "LOOT 10 MEDKITS", CurrentProgress = 2, Goal = 10 }
            };
            _context.UserMissions.AddRange(defaults);
            await _context.SaveChangesAsync();
            return Ok(defaults);
        }
            
        return Ok(missions);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateMission([FromBody] MissionUpdate update)
    {
        var mission = await _context.UserMissions
            .FirstOrDefaultAsync(m => m.Username == update.Username && m.Title == update.MissionTitle);
            
        if (mission == null) return NotFound("Mission not found");

        mission.CurrentProgress += update.ProgressGained;
        if (mission.CurrentProgress >= mission.Goal)
        {
            mission.CurrentProgress = mission.Goal;
            mission.IsCompleted = true;
        }

        await _context.SaveChangesAsync();
        return Ok(mission);
    }
}

public class MissionUpdate
{
    public string Username { get; set; }
    public string MissionTitle { get; set; }
    public int ProgressGained { get; set; }
}
