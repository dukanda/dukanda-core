// File: src/Application/Tours/Commands/RemoveTourAttraction/RemoveTourAttractionCommand.cs

using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;

namespace DukandaCore.Application.Tours.Commands.RemoveTourAttraction;

public record RemoveTourAttractionCommand : IRequest<Result>
{
    public Guid TourId { get; init; }
    public Guid TouristAttractionId { get; init; }
}

public class RemoveTourAttractionCommandHandler : IRequestHandler<RemoveTourAttractionCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public RemoveTourAttractionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(RemoveTourAttractionCommand request, CancellationToken cancellationToken)
    {
        var tour = await _context.Tours
            .Include(x=>x.Attractions)
            .FirstOrDefaultAsync(t => t.Id == request.TourId, cancellationToken);

        if (tour == null)
            return Result.Failure<TourDto>(ErrorCodes.ResourceNotFound);
        
        var existingAttraction = tour.Attractions
            .Any(ta =>
                ta.Id == request.TouristAttractionId);

        if (!existingAttraction)
            return Result.Failure<TourDto>("Atração não adicionada ao tour");

        tour.Attractions.Remove(
            await _context
                .TouristAttractions.FirstAsync(x => x.Id == request.TouristAttractionId, cancellationToken));


        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}