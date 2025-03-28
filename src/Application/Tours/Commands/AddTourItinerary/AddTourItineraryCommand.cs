using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Domain.Entities;

public record AddTourItineraryCommand : IRequest<Result<TourItineraryDto>>
{
    public Guid TourId { get; init; }
    public DateTime Date { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int DisplayOrder { get; set; }
}

public class AddTourItineraryCommandHandler : IRequestHandler<AddTourItineraryCommand, Result<TourItineraryDto>>
{
    private readonly IApplicationDbContext _context;

    public AddTourItineraryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Result<TourItineraryDto>> Handle(AddTourItineraryCommand request,
        CancellationToken cancellationToken)
    {
        var tour = await _context.Tours
            .Include(t => t.Itineraries)
            .FirstOrDefaultAsync(t => t.Id == request.TourId, cancellationToken);

        var itinerary = new TourItinerary
        {
            TourId = request.TourId,
            Date = request.Date,
            Title = request.Title,
            Description = request.Description,
            DisplayOrder = request.DisplayOrder,
        };

        _context.TourItineraries.Add(itinerary);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(new TourItineraryDto(itinerary));
    }
}