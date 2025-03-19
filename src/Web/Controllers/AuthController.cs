using DukandaCore.Application.Auth.Commands.Login;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace DukandaCore.Web.Controllers;

public class AuthController(ISender sender) : BaseController(sender)
{
    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterUserCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return Unauthorized(result.Error);
        return Ok(result.Data);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return Unauthorized(result.Error);
        return Ok(result.Data);
    }

    [HttpPost("verify-email")]
    public async Task<ActionResult> VerifyEmail(VerifyEmailCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return Unauthorized(result.Error);
        return NoContent();
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult> RefreshToken(RefreshTokenCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return Unauthorized(result.Error);
        return NoContent();
    }
}