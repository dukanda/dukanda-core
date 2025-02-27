using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Mappings;
using DukandaCore.Application.Common.Models;

public record GetCitiesQuery : IRequest<Result<PaginatedList<CityDto>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? SearchTerm { get; init; }
}

public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, Result<PaginatedList<CityDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetCitiesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PaginatedList<CityDto>>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Cities.AsQueryable();

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            query = query.Where(c => 
                c.Name.Contains(request.SearchTerm) || 
                c.Description.Contains(request.SearchTerm)
            );
        }

        var cities = await query
            .OrderBy(c => c.Name)
            .Select(c => new CityDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                Latitude = c.Latitude,
                Longitude = c.Longitude
            })
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return Result.Success(cities);
    }
}

public record CityDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
