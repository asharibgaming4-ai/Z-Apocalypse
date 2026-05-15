using projectaaa.Data;
using projectaaa.Models;
using System.Linq;

namespace projectaaa.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Users.Any(u => u.Username == "HAFF"))
        {
            context.Users.Add(new User 
            { 
                Username = "HAFF", 
                Email = "haff@test.com", 
                PasswordHash = "test1234",
                Coins = 10000
            });
            context.SaveChanges();
        }

        if (context.Items.Any()) return;

        context.Items.AddRange(
            new Item { Name = "AK-47 STEEL", Price = 2100, Category = "Weapon", ImageUrl = "assets/gun 1.png", Color = "#ff0000" },
            new Item { Name = "M4A1 TACTICAL", Price = 2400, Category = "Weapon", ImageUrl = "assets/gun 5.png", Color = "#39ff14" },
            new Item { Name = "AWM SNIPER", Price = 4200, Category = "Weapon", ImageUrl = "assets/gun 7.png", Color = "#8000ff" },
            new Item { Name = "TACTICAL AXE", Price = 750, Category = "Weapon", ImageUrl = "assets/axe.png", Color = "#cc0000" },
            new Item { Name = "COMBAT SWORD", Price = 3000, Category = "Weapon", ImageUrl = "assets/sword.png", Color = "#00ccff" },
            new Item { Name = "GLOCK 18", Price = 1000, Category = "Weapon", ImageUrl = "assets/gun 2.png", Color = "#00f2ff" },
            new Item { Name = "DESERT EAGLE", Price = 1300, Category = "Weapon", ImageUrl = "assets/gun 3.png", Color = "#ffd700" },
            new Item { Name = "WATER BOTTLE", Price = 80, Category = "Survival", ImageUrl = "assets/food 1.png", Color = "#00f2ff" },
            new Item { Name = "FIRST AID KIT", Price = 400, Category = "Survival", ImageUrl = "assets/food 3.png", Color = "#ff0000" }
        );

        context.SaveChanges();
    }
}
