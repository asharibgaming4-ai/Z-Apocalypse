using projectaaa.Data;
using projectaaa.Models;
using Microsoft.EntityFrameworkCore;

namespace projectaaa.Services;

public class AuthService
{
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> Authenticate(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user != null && user.PasswordHash == password)
        {
            return user;
        }
        return null;
    }

    public async Task<User?> Register(string username, string email, string password)
    {
        if (await _context.Users.AnyAsync(u => u.Username == username))
            return null;

        var user = new User
        {
            Username = username,
            Email = email,
            PasswordHash = password
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
}
