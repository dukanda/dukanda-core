namespace DukandaCore.Application.Auth.Commands.Login.Dto;

public class LoginResponseDto
{
    public string Token { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}
