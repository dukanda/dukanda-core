using DukandaCore.Application.Common.Interfaces;

namespace DukandaCore.Application.Tours.Commands.AddTourAttraction;

public class AddTourAttractionCommandValidator : AbstractValidator<AddTourAttractionCommand>
{
    private readonly IApplicationDbContext _context;

    public AddTourAttractionCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.TourId)
            .MustAsync(TourExists).WithMessage("O tour não existe");

        RuleFor(x => x.TouristAttractionId)
            .MustAsync(TouristAttractionExists).WithMessage("A atração turística não existe");
    }

    private async Task<bool> TourExists(Guid tourId, CancellationToken cancellationToken)
    {
        return await _context.Tours.AnyAsync(t => t.Id == tourId, cancellationToken);
    }

    private async Task<bool> TouristAttractionExists(Guid attractionId, CancellationToken cancellationToken)
    {
        return await _context.TouristAttractions.AnyAsync(ta => ta.Id == attractionId, cancellationToken);
    }
}