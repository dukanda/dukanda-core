// File: src/Application/Tours/Commands/AddTourAttraction/AddTourAttractionCommand.cs

using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Application.Tours.Commands.RemoveTourAttraction;

namespace DukandaCore.Application.Tours.Commands.AddTourAttraction;

public record AddTourAttractionCommand : IRequest<Result<TourDto>>
{
    public Guid TourId { get; init; }
    public Guid TouristAttractionId { get; init; }
}

public class AddTourAttractionCommandHandler : IRequestHandler<AddTourAttractionCommand, Result<TourDto>>
{
    private readonly IApplicationDbContext _context;

    public AddTourAttractionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<TourDto>> Handle(AddTourAttractionCommand request,
        CancellationToken cancellationToken)
    {
        // Verify tour exists
        var tour = await _context.Tours
            .Include(x=>x.Attractions)
            .FirstOrDefaultAsync(t => t.Id == request.TourId, cancellationToken);

        if (tour == null)
            return Result.Failure<TourDto>(ErrorCodes.ResourceNotFound);

        // Check if attraction is already added to the tour
        var existingAttraction = tour.Attractions
            .Any(ta =>
                ta.Id == request.TouristAttractionId);

        if (existingAttraction)
            return Result.Failure<TourDto>("Atração já adicionada ao tour");

        tour.Attractions.Add(
            await _context
                .TouristAttractions.FirstAsync(x => x.Id == request.TouristAttractionId, cancellationToken));


        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(new TourDto(tour));
    }
}