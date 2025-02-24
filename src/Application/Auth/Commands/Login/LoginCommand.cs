using DukandaCore.Application.Auth.Commands.Login.Dto;
using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;

namespace DukandaCore.Application.Auth.Commands.Login;

public record LoginCommand : IRequest<Result<LoginResponseDto>>
{
    public string Email { get; init; } = default!;
    public string Password { get; init; } = default!;
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponseDto>>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var (success, token, refreshToken) =
            await _authService.LoginAsync(request.Email, request.Password);

        if (!success)
            return Result.Failure<LoginResponseDto>(ErrorCodes.InvalidCredentials);

        return Result.Success(new LoginResponseDto
        {
            Token = token,
            RefreshToken = refreshToken
        });
    }
}