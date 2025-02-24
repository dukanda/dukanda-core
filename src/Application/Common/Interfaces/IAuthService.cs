using System.Threading.Tasks;
using DukandaCore.Application.Common.Models;

namespace DukandaCore.Application.Common.Interfaces;

public interface IAuthService
{
    Task<(bool success, string token, string refreshToken)> LoginAsync(string email, string password);
    Task ConfirmEmailAsync(string userId, string token);
    Task ForgotPasswordAsync(string email);
    Task ResetPasswordAsync(string userId, string token, string newPassword);
    Task<string> RefreshTokenAsync(string refreshToken);
    Task LogoutAsync(string userId);
}