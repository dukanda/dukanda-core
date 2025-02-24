using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;

public record GetFeaturedToursQuery : IRequest<Result<List<TourDto>>>;

public class GetFeaturedToursQueryHandler : IRequestHandler<GetFeaturedToursQuery, Result<List<TourDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetFeaturedToursQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<TourDto>>> Handle(GetFeaturedToursQuery request, CancellationToken cancellationToken)
    {
        var tours = await _context.Tours
            .Include(t => t.Agency)
            .Include(t => t.TourTypes)
            .Include(t => t.Packages)
            .OrderByDescending(t => t.Reviews.Average(r => r.Rating))
            .Take(10)
            .ToListAsync(cancellationToken);

        return Result.Success(_mapper.Map<List<TourDto>>(tours));
    }
}
