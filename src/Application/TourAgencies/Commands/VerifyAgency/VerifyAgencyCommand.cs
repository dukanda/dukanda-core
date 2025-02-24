using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;

public record VerifyAgencyCommand : IRequest<Result>
{
    public Guid UserId { get; init; }
    public bool IsVerified { get; init; }
}

public class VerifyAgencyCommandHandler : IRequestHandler<VerifyAgencyCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public VerifyAgencyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(VerifyAgencyCommand request, CancellationToken cancellationToken)
    {
        var agency = await _context.TourAgencies.FindAsync(request.UserId);
        if (agency == null)
            return Result.Failure(ErrorCodes.ResourceNotFound);

        agency.IsVerified = request.IsVerified;
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
