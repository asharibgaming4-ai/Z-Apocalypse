using System.ComponentModel.DataAnnotations;

namespace projectaaa.Models;

public class SurvivalSession
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Username { get; set; }

    // Which map/level was played
    public string MapName { get; set; } = "Training Camp";

    // How long did the player survive (in seconds)
    public int SurvivalTimeSeconds { get; set; }

    // Total zombie kills this session
    public int Kills { get; set; }

    // Coins collected this session
    public int CoinsCollected { get; set; }

    // Diamonds collected this session
    public int DiamondsCollected { get; set; }

    // Did the player survive the full timer?
    public bool Survived { get; set; }

    // When was this session played
    public DateTime PlayedAt { get; set; } = DateTime.UtcNow;
}
