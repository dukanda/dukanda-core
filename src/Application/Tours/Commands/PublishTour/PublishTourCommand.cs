using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;

public record PublishTourCommand : IRequest<Result<TourDto>>
{
    public Guid Id { get; init; }
}

public class PublishTourCommandHandler : IRequestHandler<PublishTourCommand, Result<TourDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _currentUser;

    public PublishTourCommandHandler(IApplicationDbContext context, IUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Result<TourDto>> Handle(PublishTourCommand request, CancellationToken cancellationToken)
    {
        var tour = await _context.Tours
            .Include(t => t.TourTypes)
            .Include(t => t.Agency)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (tour == null)
            return Result.Failure<TourDto>(ErrorCodes.ResourceNotFound);

        tour.PublishedAt = DateTimeOffset.UtcNow;
        tour.PublishedById = _currentUser.Id!.Value;

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(new TourDto(tour));
    }
}
