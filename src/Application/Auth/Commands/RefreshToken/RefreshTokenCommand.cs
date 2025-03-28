using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;

public record RefreshTokenCommand : IRequest<Result<string>>
{
    public string RefreshToken { get; init; } = default!;
}

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<string>>
{
    private readonly IAuthService _authService;
    private readonly IApplicationDbContext _context;

    public RefreshTokenCommandHandler(IAuthService authService, IApplicationDbContext context)
    {
        _authService = authService;
        _context = context;
    }

    public async Task<Result<string>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var token = await _authService.RefreshTokenAsync(request.RefreshToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success(token);
        }
        catch (Exception)
        {
            return Result.Failure<string>(ErrorCodes.InvalidOrExpiredToken);
        }
    }
}
