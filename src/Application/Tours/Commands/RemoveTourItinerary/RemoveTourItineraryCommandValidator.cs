using DukandaCore.Application.Common.Interfaces;

namespace DukandaCore.Application.Tours.Commands.RemoveTourItinerary;

public class RemoveTourItineraryCommandValidator : AbstractValidator<RemoveTourItineraryCommand>
{
    private readonly IApplicationDbContext _context;

    public RemoveTourItineraryCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.ItineraryId)
            .MustAsync(ItineraryExists).WithMessage("O itinerário não existe");
    }

    private async Task<bool> ItineraryExists(Guid itineraryId, CancellationToken cancellationToken)
    {
        return await _context.TourItineraries.AnyAsync(i => i.Id == itineraryId, cancellationToken);
    }
}