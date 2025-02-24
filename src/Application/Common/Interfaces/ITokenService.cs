using DukandaCore.Domain.Identity;

namespace DukandaCore.Application.Common.Interfaces;

public interface ITokenService
{
    string GenerateJwtToken(User user);
    string GenerateRefreshToken();
    bool ValidateToken(string token);
}
