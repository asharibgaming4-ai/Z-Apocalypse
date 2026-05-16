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
                // DEBUG LOGGING
                Console.WriteLine($"[DEBUG] DB PasswordHash: '{user.PasswordHash}' | Input Password: '{password}'");

                // Double-Layer Match: Check plain text OR BCrypt hash
                bool isValid = (user.PasswordHash == password.Trim());

                if (!isValid)
                {
                    try {
                        isValid = BCrypt.Net.BCrypt.Verify(password.Trim(), user.PasswordHash);
                    } catch {
                        // Ignore verify errors for plain text entries
                    }
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

            // Save raw password directly for absolute robustness during dev testing
            var user = new User
            {
                Username = username.Trim(),
                Email = email.Trim(),
                PasswordHash = password.Trim() 
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
