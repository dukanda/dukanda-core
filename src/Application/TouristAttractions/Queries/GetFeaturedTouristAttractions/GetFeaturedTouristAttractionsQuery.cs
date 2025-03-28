using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;

public record GetFeaturedTouristAttractionsQuery : IRequest<Result<List<TouristAttractionDto>>>;

public class GetFeaturedTouristAttractionsQueryHandler : IRequestHandler<GetFeaturedTouristAttractionsQuery, Result<List<TouristAttractionDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetFeaturedTouristAttractionsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<TouristAttractionDto>>> Handle(GetFeaturedTouristAttractionsQuery request, CancellationToken cancellationToken)
    {
        var featuredAttractions = await _context.TouristAttractions
            .Include(t => t.City)
            .Where(t => t.IsFeatured)
            .OrderBy(t => t.Name)
            .Select(t => new TouristAttractionDto(t))
            .ToListAsync(cancellationToken);

        return Result<List<TouristAttractionDto>>.Success(featuredAttractions);
    }
}
