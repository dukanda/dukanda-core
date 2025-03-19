using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;

public record GetTourDetailsQuery : IRequest<Result<TourDetailDto>>
{
    public Guid TourId { get; init; }
}

public class GetTourDetailsQueryHandler : IRequestHandler<GetTourDetailsQuery, Result<TourDetailDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTourDetailsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<TourDetailDto>> Handle(GetTourDetailsQuery request, CancellationToken cancellationToken)
    {
        var tour = await _context.Tours
            .Include(t => t.Agency)
            .Include(t => t.TourTypes)
            .Include(t => t.Itineraries)
            .Include(t => t.City)
            .Include(t => t.Attractions)
               .ThenInclude(t => t.City)
            .Include(t => t.Packages)
                .ThenInclude(p => p.Benefits)
            .Include(t => t.Gallery)
            .FirstOrDefaultAsync(t => t.Id == request.TourId, cancellationToken);

        if (tour == null)
            return Result.Failure<TourDetailDto>(ErrorCodes.ResourceNotFound);

        return Result.Success(new TourDetailDto(tour));
    }
}