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
            
        bool needsMigration = missions.Any(m => m.Title == "15 HEADSHOTS" || m.Title == "SURVIVE Z-WAVE" || m.Title == "LOOT 10 MEDKITS");
        
        if (!missions.Any() || needsMigration)
        {
            if (needsMigration)
            {
                var oldMissions = missions.Where(m => m.Title == "15 HEADSHOTS" || m.Title == "SURVIVE Z-WAVE" || m.Title == "LOOT 10 MEDKITS");
                _context.UserMissions.RemoveRange(oldMissions);
                await _context.SaveChangesAsync();
            }

            var existingNew = await _context.UserMissions
                .Where(m => m.Username == username && (m.Title == "SURVIVAL TIME" || m.Title == "TOTAL KILLS" || m.Title == "COLLECTED COINS"))
                .ToListAsync();

            var defaults = new List<UserMission>();
            if (!existingNew.Any(m => m.Title == "SURVIVAL TIME"))
                defaults.Add(new UserMission { Username = username, Title = "SURVIVAL TIME", CurrentProgress = 0, Goal = 180 });
            if (!existingNew.Any(m => m.Title == "TOTAL KILLS"))
                defaults.Add(new UserMission { Username = username, Title = "TOTAL KILLS", CurrentProgress = 0, Goal = 50 });
            if (!existingNew.Any(m => m.Title == "COLLECTED COINS"))
                defaults.Add(new UserMission { Username = username, Title = "COLLECTED COINS", CurrentProgress = 0, Goal = 1000 });

            if (defaults.Any())
            {
                _context.UserMissions.AddRange(defaults);
                await _context.SaveChangesAsync();
            }

            missions = await _context.UserMissions
                .Where(m => m.Username == username)
                .ToListAsync();
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
