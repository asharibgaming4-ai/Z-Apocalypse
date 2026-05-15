using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectaaa.Data;
using projectaaa.Models;

namespace projectaaa.Controllers;

[ApiController]
[Route("api/store")]
public class StoreController : ControllerBase
{
    private readonly AppDbContext _context;

    public StoreController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("items")]
    public async Task<IActionResult> GetItems()
    {
        var items = await _context.Items.ToListAsync();
        return Ok(items);
    }

    [HttpPost("purchase")]
    public async Task<IActionResult> PurchaseItem([FromBody] PurchaseRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
        if (user == null) return NotFound(new { message = "User not found" });

        var item = await _context.Items.FindAsync(request.ItemId);
        if (item == null) return NotFound(new { message = "Item not found" });

        // Check if user already owns it
        var alreadyOwned = await _context.UserInventories
            .AnyAsync(ui => ui.UserId == user.Id && ui.ItemId == item.Id);
        
        if (alreadyOwned) return BadRequest(new { message = "ALREADY OWNED" });

        // Check coins
        if (user.Coins < item.Price)
        {
            return BadRequest(new { message = "ACCESS DENIED: INSUFFICIENT CREDITS" });
        }

        // Subtract coins
        user.Coins -= item.Price;

        // Add to inventory
        var inventoryItem = new UserInventory
        {
            UserId = user.Id,
            ItemId = item.Id
        };

        _context.UserInventories.Add(inventoryItem);

        // Save changes
        await _context.SaveChangesAsync();

        return Ok(new { 
            message = "TRANSACTION COMPLETE: ITEM ADDED TO INVENTORY", 
            newBalance = user.Coins 
        });
    }
}

public class PurchaseRequest
{
    public string Username { get; set; }
    public int ItemId { get; set; }
}
