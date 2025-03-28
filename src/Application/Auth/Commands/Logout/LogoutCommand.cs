using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;

public record LogoutCommand : IRequest<Result>
{
    public string UserId { get; init; } = default!;
}

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result>
{
    private readonly IAuthService _authService;
    private readonly IApplicationDbContext _context;

    public LogoutCommandHandler(IAuthService authService, IApplicationDbContext context)
    {
        _authService = authService;
        _context = context;
    }

    public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _authService.LogoutAsync(request.UserId);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(ErrorCodes.UserNotFound);
        }
    }
}
