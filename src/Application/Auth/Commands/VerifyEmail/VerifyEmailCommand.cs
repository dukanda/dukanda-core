using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;

public record VerifyEmailCommand : IRequest<Result>
{
    public string UserId { get; init; } = default!;
    public string Token { get; init; } = default!;
}

public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, Result>
{
    private readonly IAuthService _authService;
    private readonly IApplicationDbContext _context;

    public VerifyEmailCommandHandler(IAuthService authService, IApplicationDbContext context)
    {
        _authService = authService;
        _context = context;
    }

    public async Task<Result> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _authService.ConfirmEmailAsync(request.UserId, request.Token);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(ErrorCodes.InvalidOrExpiredToken);
        }
    }
}
