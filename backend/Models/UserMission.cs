using System.ComponentModel.DataAnnotations;

namespace projectaaa.Models;

public class UserMission
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Title { get; set; }

    public int CurrentProgress { get; set; }

    [Required]
    public int Goal { get; set; }

    public bool IsCompleted { get; set; } = false;
}
