using projectaaa.Data;
using projectaaa.Models;
using System.Linq;
using BCrypt.Net;

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
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("test1234"),
                Coins = 25000,
                Level = 1,
                SelectedCharacter = "GASMASK",
                SelectedSkin = "GASMASK",
                IsMedeaUnlocked = true
            });
            context.Users.Add(new User { 
                Username = "WE3", 
                Email = "we3@test.com", 
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("test1234"),
                Coins = 25000,
                Level = 1,
                SelectedCharacter = "GASMASK",
                SelectedSkin = "GASMASK",
                IsMedeaUnlocked = true
            });
            context.SaveChanges();
        }
        else
        {
            var haff = context.Users.FirstOrDefault(u => u.Username == "HAFF");
            if (haff != null)
            {
                haff.SelectedCharacter = "GASMASK";
                haff.SelectedSkin = "GASMASK";
                haff.IsMedeaUnlocked = true;
                haff.Level = 1;
                // Migrate plain text passwords to BCrypt hash
                if (!haff.PasswordHash.StartsWith("$2"))
                {
                    haff.PasswordHash = BCrypt.Net.BCrypt.HashPassword(haff.PasswordHash);
                }
            }
            var we3 = context.Users.FirstOrDefault(u => u.Username == "WE3");
            if (we3 != null)
            {
                we3.SelectedCharacter = "GASMASK";
                we3.SelectedSkin = "GASMASK";
                we3.IsMedeaUnlocked = true;
                we3.Level = 1;
                // Migrate plain text passwords to BCrypt hash
                if (!we3.PasswordHash.StartsWith("$2"))
                {
                    we3.PasswordHash = BCrypt.Net.BCrypt.HashPassword(we3.PasswordHash);
                }
            }
            context.SaveChanges();
        }

        var items = new[]
        {
            new Item { Name = "AK-47 STEEL", Price = 0, Category = "Weapon", ImageUrl = "assets/gun 1.png", Color = "#ff0000" },
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
            var existing = context.Items.FirstOrDefault(i => i.Name == item.Name);
            if (existing == null)
            {
                context.Items.Add(item);
            }
            else if (item.Name == "AK-47 STEEL" && existing.Price != 0)
            {
                existing.Price = 0;
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

        // Remove legacy missions for HAFF if they exist
        var legacyMissions = context.UserMissions
            .Where(m => m.Username == "HAFF" && (m.Title == "15 HEADSHOTS" || m.Title == "SURVIVE Z-WAVE" || m.Title == "LOOT 10 MEDKITS"))
            .ToList();
        if (legacyMissions.Any())
        {
            context.UserMissions.RemoveRange(legacyMissions);
            context.SaveChanges();
        }

        if (!context.UserMissions.Any(m => m.Username == "HAFF"))
        {
            context.UserMissions.AddRange(
                new UserMission { Username = "HAFF", Title = "SURVIVAL TIME", CurrentProgress = 0, Goal = 180 },
                new UserMission { Username = "HAFF", Title = "TOTAL KILLS", CurrentProgress = 0, Goal = 50 },
                new UserMission { Username = "HAFF", Title = "COLLECTED COINS", CurrentProgress = 0, Goal = 1000 }
            );
        }

        context.SaveChanges();
    }
}
