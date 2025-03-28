using DukandaCore.Application.Auth.Commands.Login;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DukandaCore.Application.Users.Queries.GetUserById;
using DukandaCore.Application.Users.Queries.GetCurrentUser;
using Microsoft.AspNetCore.Authorization;

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
        //Adicionar redirect frontend
        return Redirect("dukanda.ao");
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult> RefreshToken(RefreshTokenCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return Unauthorized(result.Error);
        return NoContent();
    }

    [HttpGet("users/{userId}")]
    public async Task<ActionResult> GetUser(Guid userId)
    {
        var result = await _sender.Send(new GetUserByIdQuery(userId));
        if (!result.IsSuccess)
            return NotFound(result.Error);
        return Ok(result.Data);
    }

    [HttpGet("users/me")]
    [Authorize]
    public async Task<ActionResult> GetCurrentUser()
    {
        var result = await _sender.Send(new GetCurrentUserQuery());
        if (!result.IsSuccess)
            return Unauthorized(result.Error);
        return Ok(result.Data);
    }
}