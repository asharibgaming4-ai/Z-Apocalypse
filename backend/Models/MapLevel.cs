using System.ComponentModel.DataAnnotations;

namespace projectaaa.Models;

public class MapLevel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string MapName { get; set; }

    [Required]
    public int RequiredLevel { get; set; }

    [Required]
    public string Difficulty { get; set; }

    public string ImageUrl { get; set; }
}
