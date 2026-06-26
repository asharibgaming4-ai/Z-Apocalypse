using projectaaa.Data;
using projectaaa.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace projectaaa.Services;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly ILogger<AuthService> _logger;

    public AuthService(AppDbContext context, ILogger<AuthService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<User?> Authenticate(string username, string password)
    {
        try 
        {
            // Case-insensitive lookup with Trim
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower().Trim());
            
            if (user != null)
            {
                bool isValid = false;

                // First try BCrypt verification (for already-hashed passwords)
                try {
                    isValid = BCrypt.Net.BCrypt.Verify(password.Trim(), user.PasswordHash);
                } catch {
                    // If BCrypt fails, it means the stored password is plain text (legacy)
                }

                // Fallback: check plain text match (for legacy/seed data passwords)
                if (!isValid && user.PasswordHash == password.Trim())
                {
                    isValid = true;

                    // Migrate: Re-hash the plain text password to BCrypt
                    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password.Trim());
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"[AUTH] Migrated plain-text password to BCrypt for user: {user.Username}");
                }

                if (isValid) return user;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Auth crash for {Username}", username);
        }
        return null;
    }

    public async Task<User?> Register(string username, string email, string password)
    {
        try 
        {
            if (await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower().Trim()))
                return null;

            // Hash the password with BCrypt before storing
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password.Trim());

            var user = new User
            {
                Username = username.Trim(),
                Email = email.Trim(),
                PasswordHash = hashedPassword
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Register crash for {Username}", username);
            throw;
        }
    }
}

