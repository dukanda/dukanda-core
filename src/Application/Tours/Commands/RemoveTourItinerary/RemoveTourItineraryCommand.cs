using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;

namespace DukandaCore.Application.Tours.Commands.RemoveTourItinerary;

public record RemoveTourItineraryCommand : IRequest<Result>
{
    public Guid ItineraryId { get; init; }
}

public class RemoveTourItineraryCommandHandler : IRequestHandler<RemoveTourItineraryCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public RemoveTourItineraryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(RemoveTourItineraryCommand request, CancellationToken cancellationToken)
    {
        var itinerary = await _context.TourItineraries
            .FirstOrDefaultAsync(i => i.Id == request.ItineraryId, cancellationToken);

        if (itinerary == null)
            return Result.Failure(ErrorCodes.ResourceNotFound);

        _context.TourItineraries.Remove(itinerary);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}