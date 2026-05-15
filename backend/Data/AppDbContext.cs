using Microsoft.EntityFrameworkCore;
using projectaaa.Models;

namespace projectaaa.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<UserInventory> UserInventories { get; set; }
}
