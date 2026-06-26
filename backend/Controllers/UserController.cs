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

        await EnsureFirstGunOwnedAndEquipped(user);

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

        await EnsureFirstGunOwnedAndEquipped(user);

        var inventoryItemIds = await _context.UserInventories
            .Where(ui => ui.UserId == user.Id)
            .Select(ui => ui.ItemId)
            .ToListAsync();

        var items = await _context.Items
            .Where(i => inventoryItemIds.Contains(i.Id))
            .ToListAsync();

        return Ok(items);
    }

    private async Task EnsureFirstGunOwnedAndEquipped(User user)
    {
        var firstGun = await _context.Items.FirstOrDefaultAsync(i => i.Name == "AK-47 STEEL");
        if (firstGun != null)
        {
            var ownsFirstGun = await _context.UserInventories
                .AnyAsync(ui => ui.UserId == user.Id && ui.ItemId == firstGun.Id);
            
            bool changed = false;
            if (!ownsFirstGun)
            {
                _context.UserInventories.Add(new UserInventory { UserId = user.Id, ItemId = firstGun.Id });
                changed = true;
            }

            if (user.EquippedWeaponId == null || user.EquippedWeaponId == 0)
            {
                user.EquippedWeaponId = firstGun.Id;
                changed = true;
            }

            if (changed)
            {
                await _context.SaveChangesAsync();
            }
        }
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

    [HttpPost("add-coins")]
    public async Task<IActionResult> AddCoins([FromBody] AddCoinsRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
        if (user == null) return NotFound(new { message = "User not found" });

        user.Coins += request.Coins;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Coins updated successfully", coins = user.Coins });
    }
}

public class EquipRequest
{
    public string Username { get; set; }
    public int ItemId { get; set; }
}

public class AddCoinsRequest
{
    public string Username { get; set; }
    public int Coins { get; set; }
}
