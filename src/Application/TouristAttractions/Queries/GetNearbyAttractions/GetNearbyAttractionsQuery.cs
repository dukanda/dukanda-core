using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace DukandaCore.Application.TouristAttractions.Queries.GetNearbyAttractions;

public record GetNearbyAttractionsQuery : IRequest<Result<List<TouristAttractionDto>>>
{
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public double RadiusInKm { get; init; } = 5;
}

public class GetNearbyAttractionsQueryHandler : IRequestHandler<GetNearbyAttractionsQuery, Result<List<TouristAttractionDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetNearbyAttractionsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<TouristAttractionDto>>> Handle(GetNearbyAttractionsQuery request, CancellationToken cancellationToken)
    {
        // Convert radius from km to degrees (approximate)
        var radiusInDegrees = request.RadiusInKm / 111.0; // 1 degree ≈ 111km at equator

        var nearbyAttractions = await _context.TouristAttractions
            .Include(t => t.City)
            .Where(t => 
                Math.Pow(t.Latitude - request.Latitude, 2) + 
                Math.Pow(t.Longitude - request.Longitude, 2) <= 
                Math.Pow(radiusInDegrees, 2))
            .OrderBy(t => 
                Math.Pow(t.Latitude - request.Latitude, 2) + 
                Math.Pow(t.Longitude - request.Longitude, 2))
            .Take(20) // Limit results
            .ToListAsync(cancellationToken);

        return Result.Success(nearbyAttractions.Select(a => new TouristAttractionDto(a)).ToList());
    }
}