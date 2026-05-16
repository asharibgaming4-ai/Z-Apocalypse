using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectaaa.Data;
using projectaaa.Models;

namespace projectaaa.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("profile/{username}")]
    public async Task<IActionResult> GetProfile(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) return NotFound(new { message = "User not found" });

        return Ok(new { 
            username = user.Username, 
            coins = user.Coins, 
            skullTokens = user.SkullTokens,
            level = user.Level,
            experience = user.Experience,
            equippedWeaponId = user.EquippedWeaponId 
        });
    }

    [HttpGet("inventory/{username}")]
    public async Task<IActionResult> GetInventory(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) return NotFound(new { message = "User not found" });

        var inventoryItemIds = await _context.UserInventories
            .Where(ui => ui.UserId == user.Id)
            .Select(ui => ui.ItemId)
            .ToListAsync();

        var items = await _context.Items
            .Where(i => inventoryItemIds.Contains(i.Id))
            .ToListAsync();

        return Ok(items);
    }

    [HttpPost("equip")]
    public async Task<IActionResult> EquipItem([FromBody] EquipRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
        if (user == null) return NotFound(new { message = "User not found" });

        user.EquippedWeaponId = request.ItemId;
        await _context.SaveChangesAsync();

        return Ok(new { message = "EQUIPPED SUCCESSFUL", equippedWeaponId = user.EquippedWeaponId });
    }
}

public class EquipRequest
{
    public string Username { get; set; }
    public int ItemId { get; set; }
}
