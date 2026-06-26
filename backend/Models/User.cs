using System.ComponentModel.DataAnnotations;

namespace projectaaa.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    public int Coins { get; set; } = 10000;
    public int SkullTokens { get; set; } = 10;
    public int Level { get; set; } = 1;
    public int Experience { get; set; } = 0;
    public int? EquippedWeaponId { get; set; }

    // Character info
    public string SelectedCharacter { get; set; } = "GASMASK";
    public bool IsMedeaUnlocked { get; set; } = true;

    // Stats
    public int Damage { get; set; } = 85;
    public int Speed { get; set; } = 40;
    public int Accuracy { get; set; } = 65;
    public int Armor { get; set; } = 90;

    // Settings - Audio
    public int MasterVolume { get; set; } = 80;
    public int MusicVolume { get; set; } = 60;
    public int SfxVolume { get; set; } = 90;

    // Settings - Graphics
    public string GraphicsQuality { get; set; } = "Ultra";
    public bool FullscreenMode { get; set; } = true;
    public int RenderScale { get; set; } = 100;

    // Backward compatibility
    public string SelectedSkin { get; set; } = "GASMASK";
    public int MaxHealth { get; set; } = 100;
    public int MovementSpeed { get; set; } = 10;
    public int Stamina { get; set; } = 100;
}
