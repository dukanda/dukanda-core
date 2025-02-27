using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Mappings;
using DukandaCore.Application.Common.Models;

public record GetTourTypesQuery : IRequest<Result<PaginatedList<TourTypeDto>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? SearchTerm { get; init; }
}

public class GetTourTypesQueryHandler : IRequestHandler<GetTourTypesQuery, Result<PaginatedList<TourTypeDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetTourTypesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PaginatedList<TourTypeDto>>> Handle(GetTourTypesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.TourTypes.AsQueryable();

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            query = query.Where(t => 
                t.Name.Contains(request.SearchTerm) || 
                t.Description.Contains(request.SearchTerm)
            );
        }

        var tourTypes = await query
            .OrderBy(t => t.DisplayOrder)
            .Select(t => new TourTypeDto(t))
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return Result.Success(tourTypes);
    }
}
