using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Domain.Entities;

public record UpdateTourCommand : IRequest<Result<TourDto>>
{
    public Guid Id { get; init; }
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public decimal BasePrice { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int CityId { get; init; }
    public List<int> TourTypeIds { get; init; } = new();
}

public class UpdateTourCommandHandler : IRequestHandler<UpdateTourCommand, Result<TourDto>>
{
    private readonly IApplicationDbContext _context;

    public UpdateTourCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<TourDto>> Handle(UpdateTourCommand request, CancellationToken cancellationToken)
    {
        var tour = await _context.Tours
            .Include(t => t.TourTypes)
            .Include(t => t.Itineraries)
            .Include(t => t.Attractions)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (tour == null)
            return Result.Failure<TourDto>(ErrorCodes.ResourceNotFound);

   
        // Update Tour Types
        var tourTypes = await _context.TourTypes
            .Where(tt => request.TourTypeIds.Contains(tt.Id))
            .ToListAsync(cancellationToken);
        
        // Update basic tour information
        tour.Title = request.Title;
        tour.Description = request.Description;
        tour.BasePrice = request.BasePrice;
        tour.StartDate = request.StartDate;
        tour.EndDate = request.EndDate;
        tour.CityId = request.CityId;
        tour.TourTypes = tourTypes;
        
        
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(new TourDto(tour));
    }
}