using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Application.Common.Constants;
using DukandaCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DukandaCore.Infrastructure.Identity;

public class AuthService : IAuthService
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthService(IApplicationDbContext context, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<(bool success, string token, string refreshToken)> LoginAsync(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email)!;
        if (user == null || !_passwordHasher.VerifyPassword(user.PasswordHash!, password))
            return (false, string.Empty, string.Empty);

        var token = _tokenService.GenerateJwtToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();
        
        return (true, token, refreshToken);
    }

    public async Task ConfirmEmailAsync(string userId, string token)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            throw new InvalidOperationException(ErrorCodes.UserNotFound);

        if (user.EmailConfirmed)
            throw new InvalidOperationException(ErrorCodes.AlreadyConfirmed);

        user.EmailConfirmed = true;
    }

    public async Task ForgotPasswordAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
            throw new InvalidOperationException(ErrorCodes.UserNotFound);
       
        user.RefreshToken = _tokenService.GenerateRefreshToken();
    }

    public async Task ResetPasswordAsync(string userId, string token, string newPassword)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            throw new InvalidOperationException(ErrorCodes.UserNotFound);

        if (!_tokenService.ValidateToken(token))
            throw new InvalidOperationException(ErrorCodes.InvalidOrExpiredToken);

        user.PasswordHash = _passwordHasher.HashPassword(newPassword);
    }

    public async Task<string> RefreshTokenAsync(string refreshToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        if (user == null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            throw new InvalidOperationException(ErrorCodes.InvalidOrExpiredToken);

        return _tokenService.GenerateJwtToken(user);
    }

    public async Task LogoutAsync(string userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            throw new InvalidOperationException(ErrorCodes.UserNotFound);

        user.RefreshToken = null;
    }
}