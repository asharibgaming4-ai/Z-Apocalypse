using System.ComponentModel.DataAnnotations;

namespace projectaaa.Models;

public class UserInventory
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int ItemId { get; set; }
}
