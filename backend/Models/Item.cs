using System.ComponentModel.DataAnnotations;

namespace projectaaa.Models;

public class Item
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public int Price { get; set; }

    [Required]
    public string Category { get; set; } // e.g. Weapon, Survival

    public string ImageUrl { get; set; }

    public string Color { get; set; } // for UI theme
}
