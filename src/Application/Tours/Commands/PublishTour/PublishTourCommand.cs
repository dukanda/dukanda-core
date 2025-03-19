using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;

namespace DukandaCore.Application.Tours.Commands.PublishTour;

public record PublishTourCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; init; }
}

public class PublishTourCommandHandler : IRequestHandler<PublishTourCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _currentUser;

    public PublishTourCommandHandler(IApplicationDbContext context, IUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Result<Unit>> Handle(PublishTourCommand request, CancellationToken cancellationToken)
    {
        var tour = await _context.Tours.FindAsync(request.Id);

        if (tour == null)
            return Result.Failure<Unit>(ErrorCodes.ResourceNotFound);

        tour.PublishedById = _currentUser.Id;
        tour.PublishedAt = DateTimeOffset.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
