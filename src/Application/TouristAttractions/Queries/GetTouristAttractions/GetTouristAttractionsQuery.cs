using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Mappings;
using DukandaCore.Application.Common.Models;

public record GetTouristAttractionsQuery : IRequest<Result<PaginatedList<TouristAttractionDto>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetTouristAttractionsQueryHandler : IRequestHandler<GetTouristAttractionsQuery, Result<PaginatedList<TouristAttractionDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetTouristAttractionsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PaginatedList<TouristAttractionDto>>> Handle(GetTouristAttractionsQuery request, CancellationToken cancellationToken)
    {
        var attractions = await _context.TouristAttractions
            .Include(t => t.City)
            .OrderBy(t => t.Name)
            .Select(t => new TouristAttractionDto(t))
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return Result<PaginatedList<TouristAttractionDto>>.Success(attractions);
    }
}