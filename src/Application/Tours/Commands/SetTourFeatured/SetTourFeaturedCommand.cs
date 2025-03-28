using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;

public record SetTourFeaturedCommand : IRequest<Result<TourDto>>
{
    public Guid Id { get; init; }
    public bool IsFeatured { get; init; }
}

public class SetTourFeaturedCommandHandler : IRequestHandler<SetTourFeaturedCommand, Result<TourDto>>
{
    private readonly IApplicationDbContext _context;

    public SetTourFeaturedCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<TourDto>> Handle(SetTourFeaturedCommand request, CancellationToken cancellationToken)
    {
        var tour = await _context.Tours
            .Include(t => t.TourTypes)
            .Include(t => t.Agency)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (tour == null)
            return Result.Failure<TourDto>(ErrorCodes.ResourceNotFound);

        tour.IsFeatured = request.IsFeatured;
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(new TourDto(tour));
    }
}
