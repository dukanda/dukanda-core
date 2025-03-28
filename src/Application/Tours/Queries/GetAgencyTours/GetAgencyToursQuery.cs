using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Mappings;
using DukandaCore.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace DukandaCore.Application.Tours.Queries.GetAgencyTours;

public record GetAgencyToursQuery : IRequest<Result<PaginatedList<TourDto>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public bool OnlyPublished { get; init; } = false;
    public string? Search { get; init; }
}

public class GetAgencyToursQueryHandler : IRequestHandler<GetAgencyToursQuery, Result<PaginatedList<TourDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _currentUser;

    public GetAgencyToursQueryHandler(IApplicationDbContext context, IUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Result<PaginatedList<TourDto>>> Handle(GetAgencyToursQuery request, CancellationToken cancellationToken)
    {
        if (_currentUser.Id == null)
        {
            return Result.Failure<PaginatedList<TourDto>>("User not authenticated");
        }

        // Retrieve the agency for the current user
        var agency = await _context.TourAgencies
            .FirstOrDefaultAsync(a => a.UserId == _currentUser.Id, cancellationToken);

        if (agency == null)
        {
            return Result.Failure<PaginatedList<TourDto>>("User is not associated with any agency");
        }

        var query = _context.Tours
            .Where(t => t.AgencyId == agency.UserId)
            .Include(t => t.City)
            .Include(t => t.TourTypes)
            .Include(t => t.Agency)
            .AsQueryable();

        if (request.OnlyPublished)
        {
            query = query.Where(t => t.PublishedAt != null);
        }

        if (!string.IsNullOrEmpty(request.Search))
        {
            query = query.Where(t => t.Title.Contains(request.Search) || 
                                  t.Description.Contains(request.Search));
        }

        var tours = await query
            .OrderByDescending(t => t.Created)
            .Select(t => new TourDto(t))
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return Result<PaginatedList<TourDto>>.Success(tours);
    }
} 