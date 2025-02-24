using DukandaCore.Application.Common.Interfaces;

namespace DukandaCore.Application.Tours.Commands.RemoveTourAttraction;

public class RemoveTourAttractionCommandValidator : AbstractValidator<RemoveTourAttractionCommand>
{
    private readonly IApplicationDbContext _context;

    public RemoveTourAttractionCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x)
            .MustAsync(TourAttractionExists).WithMessage("A atração não está associada a este tour");
    }

    private async Task<bool> TourAttractionExists(RemoveTourAttractionCommand command,
        CancellationToken cancellationToken)
    {
        return await _context.Tours.AnyAsync(ta =>
                ta.Id == command.TourId &&
                ta.Attractions.Any(a => a.Id == command.TouristAttractionId),
            cancellationToken);
    }
}