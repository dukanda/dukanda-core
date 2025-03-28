using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Mappings;
using DukandaCore.Application.Common.Models;

public record GetTourAgenciesQuery : IRequest<Result<PaginatedList<TourAgencyDto>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public bool? IsVerified { get; init; }
    public int? AgencyTypeId { get; init; }
}

public class GetTourAgenciesQueryHandler : IRequestHandler<GetTourAgenciesQuery, Result<PaginatedList<TourAgencyDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTourAgenciesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<TourAgencyDto>>> Handle(GetTourAgenciesQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.TourAgencies
            .AsNoTracking()
            .Include(x => x.TourAgencyType)
            .AsQueryable();

        if (request.IsVerified.HasValue)
            query = query.Where(x => x.IsVerified == request.IsVerified);

        if (request.AgencyTypeId.HasValue)
            query = query.Where(x => x.TourAgencyTypeId == request.AgencyTypeId);

        var agencies = await query
            .OrderBy(x => x.Name)
            .Select(x => new TourAgencyDto(x))
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return Result.Success(agencies);
    }
}