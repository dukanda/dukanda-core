using System.Text.Json.Serialization;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Mappings;
using DukandaCore.Application.Common.Models;

public record GetToursQuery : IRequest<Result<PaginatedList<TourDto>>>
{
    public bool OnlyPublished { get; private set; } = true;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? SearchTerm { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
    public List<int> TourTypeIds { get; init; } = new();
    public int? CityId { get; init; }
    public bool? IsFeatured { get; init; }

    public void IncludeUnpublishedTours()
    {
         OnlyPublished = false ;
    }
}


public class GetToursQueryHandler : IRequestHandler<GetToursQuery, Result<PaginatedList<TourDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetToursQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PaginatedList<TourDto>>> Handle(GetToursQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Tours
            .Include(t => t.TourTypes)
            .Include(t => t.Agency)
            .AsQueryable();

        if (request.OnlyPublished)
        {
            query = query.Where(t => t.PublishedAt != null);
        }

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            query = query.Where(t => t.Title.Contains(request.SearchTerm) || 
                                   t.Description.Contains(request.SearchTerm));
        }
        if (request.CityId.HasValue)
        {
            query = query.Where(t => t.CityId >= request.CityId);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(t => t.StartDate >= request.StartDate);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(t => t.EndDate <= request.EndDate);
        }

        if (request.MinPrice.HasValue)
        {
            query = query.Where(t => t.BasePrice >= request.MinPrice);
        }

        if (request.MaxPrice.HasValue)
        {
            query = query.Where(t => t.BasePrice <= request.MaxPrice);
        }

        if (request.TourTypeIds.Any())
        {
            query = query.Where(t => t.TourTypes.Any(tt => request.TourTypeIds.Contains(tt.Id)));
        }

        if (request.IsFeatured.HasValue)
        {
            query = query.Where(t => t.IsFeatured == request.IsFeatured);
        }

        var tours = await query
            .OrderByDescending(t => t.Created)
            .Select(t => new TourDto(t))
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return Result.Success(tours);
    }
}