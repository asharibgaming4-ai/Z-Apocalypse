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
                PasswordHash = "test1234", // Raw plain text for testing
                Coins = 25000,
                Level = 42 
            });
            context.Users.Add(new User { 
                Username = "WE3", 
                Email = "we3@test.com", 
                PasswordHash = "test1234", // Raw plain text for testing
                Coins = 25000,
                Level = 42 
            });
            context.SaveChanges();
        }

        var items = new[]
        {
            new Item { Name = "AK-47 STEEL", Price = 2100, Category = "Weapon", ImageUrl = "assets/gun 1.png", Color = "#ff0000" },
            new Item { Name = "GLOCK 18", Price = 1000, Category = "Weapon", ImageUrl = "assets/gun 2.png", Color = "#00f2ff" },
            new Item { Name = "DESERT EAGLE", Price = 1300, Category = "Weapon", ImageUrl = "assets/gun 3.png", Color = "#ffd700" },
            new Item { Name = "SHOTGUN X-12", Price = 1800, Category = "Weapon", ImageUrl = "assets/gun 4.png", Color = "#ffffff" },
            new Item { Name = "M4A1 TACTICAL", Price = 2400, Category = "Weapon", ImageUrl = "assets/gun 5.png", Color = "#39ff14" },
            new Item { Name = "AWM SNIPER", Price = 4200, Category = "Weapon", ImageUrl = "assets/gun 7.png", Color = "#8000ff" },
            new Item { Name = "TACTICAL AXE", Price = 750, Category = "Weapon", ImageUrl = "assets/axe.png", Color = "#cc0000" },
            new Item { Name = "SURVIVAL HATCHET", Price = 600, Category = "Weapon", ImageUrl = "assets/axe 2.png", Color = "#888888" },
            new Item { Name = "HEAVY CROWBAR", Price = 500, Category = "Weapon", ImageUrl = "assets/axe 3.png", Color = "#555555" },
            new Item { Name = "MINING PICK", Price = 900, Category = "Weapon", ImageUrl = "assets/axe 4.png", Color = "#444444" },
            new Item { Name = "COMBAT SWORD", Price = 3000, Category = "Weapon", ImageUrl = "assets/sword.png", Color = "#00ccff" },
            new Item { Name = "WATER BOTTLE", Price = 80, Category = "Survival", ImageUrl = "assets/food 1.png", Color = "#00f2ff" },
            new Item { Name = "ENERGY DRINK", Price = 120, Category = "Survival", ImageUrl = "assets/food 2.png", Color = "#ffff00" },
            new Item { Name = "FIRST AID KIT", Price = 400, Category = "Survival", ImageUrl = "assets/food 3.png", Color = "#ff0000" },
            new Item { Name = "CANNED RATIONS", Price = 150, Category = "Survival", ImageUrl = "assets/food 4.png", Color = "#8b4513" },
            new Item { Name = "MRE PACK", Price = 250, Category = "Survival", ImageUrl = "assets/food 5.png", Color = "#556b2f" },
            new Item { Name = "LARGE MEDKIT", Price = 800, Category = "Survival", ImageUrl = "assets/food 6.png", Color = "#ff4500" },
            new Item { Name = "ADRENALINE", Price = 1200, Category = "Survival", ImageUrl = "assets/food 7.png", Color = "#00ff00" }
        };

        foreach (var item in items)
        {
            if (!context.Items.Any(i => i.Name == item.Name))
            {
                context.Items.Add(item);
            }
        }

        if (!context.MapLevels.Any())
        {
            context.MapLevels.AddRange(
                new MapLevel { MapName = "Training Camp", RequiredLevel = 1, Difficulty = "Easy", ImageUrl = "assets/level 1.png" },
                new MapLevel { MapName = "The Dark Tunnel", RequiredLevel = 5, Difficulty = "Normal", ImageUrl = "assets/level 2.png" },
                new MapLevel { MapName = "Cargo Yard", RequiredLevel = 10, Difficulty = "Medium", ImageUrl = "assets/level 3.png" },
                new MapLevel { MapName = "Parking Lot", RequiredLevel = 20, Difficulty = "Hard", ImageUrl = "assets/level 4.png" },
                new MapLevel { MapName = "The Rooftop", RequiredLevel = 35, Difficulty = "Expert", ImageUrl = "assets/level 5.png" }
            );
        }

        if (!context.UserMissions.Any())
        {
            context.UserMissions.AddRange(
                new UserMission { Username = "HAFF", Title = "15 HEADSHOTS", CurrentProgress = 12, Goal = 15 },
                new UserMission { Username = "HAFF", Title = "SURVIVE Z-WAVE", CurrentProgress = 0, Goal = 1 },
                new UserMission { Username = "HAFF", Title = "LOOT 10 MEDKITS", CurrentProgress = 2, Goal = 10 }
            );
        }

        context.SaveChanges();
    }
}
