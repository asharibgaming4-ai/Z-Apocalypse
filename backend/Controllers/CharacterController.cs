using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectaaa.Data;
using projectaaa.Models;

namespace projectaaa.Controllers;

[ApiController]
[Route("api/character")]
public class CharacterController : ControllerBase
{
    private readonly AppDbContext _context;

    public CharacterController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("status/{username}")]
    public async Task<IActionResult> GetCharacterStatus(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) return NotFound(new { message = "User not found" });

        return Ok(new
        {
            selectedCharacter = user.SelectedCharacter,
            skullTokens = user.SkullTokens,
            isMedeaUnlocked = user.IsMedeaUnlocked,
            stats = new
            {
                damage = user.Damage,
                speed = user.Speed,
                accuracy = user.Accuracy,
                armor = user.Armor
            }
        });
    }

    [HttpPost("unlock")]
    public async Task<IActionResult> UnlockCharacter([FromBody] UnlockRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
        if (user == null) return NotFound(new { message = "User not found" });

        // Logic for Medea (the 3rd character)
        if (request.CharacterName.ToUpper() == "MEDEA")
        {
            if (user.SkullTokens < 5000) return BadRequest(new { message = "INSUFFICIENT SKULL TOKENS" });
            
            user.SkullTokens -= 5000;
            user.IsMedeaUnlocked = true;
            await _context.SaveChangesAsync();
            return Ok(new { message = "CHARACTER UNLOCKED", skullTokens = user.SkullTokens });
        }

        return BadRequest(new { message = "Character already unlocked or invalid" });
    }

    [HttpPost("update-skin")]
    public async Task<IActionResult> UpdateSkin([FromBody] UpdateSkinRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
        if (user == null) return NotFound(new { message = "User not found" });

        user.SelectedCharacter = request.NewSkin;
        
        // Update stats based on selection (demo logic)
        if (request.NewSkin.ToUpper() == "REMY") { user.Damage = 85; user.Speed = 40; user.Accuracy = 65; user.Armor = 90; }
        else if (request.NewSkin.ToUpper() == "HUNTER") { user.Damage = 70; user.Speed = 95; user.Accuracy = 80; user.Armor = 40; }
        else if (request.NewSkin.ToUpper() == "MEDEA") { user.Damage = 50; user.Speed = 80; user.Accuracy = 60; user.Armor = 45; }

        await _context.SaveChangesAsync();

        return Ok(new { 
            message = "Character updated", 
            selectedCharacter = user.SelectedCharacter,
            stats = new { damage = user.Damage, speed = user.Speed, accuracy = user.Accuracy, armor = user.Armor }
        });
    }
}

public class UnlockRequest
{
    public string Username { get; set; }
    public string CharacterName { get; set; }
}

public class UpdateSkinRequest
{
    public string Username { get; set; }
    public string NewSkin { get; set; }
}
