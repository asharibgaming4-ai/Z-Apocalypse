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

    public int? EquippedWeaponId { get; set; }
}
