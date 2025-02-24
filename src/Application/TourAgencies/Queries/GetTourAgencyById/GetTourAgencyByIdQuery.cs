using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;

public record GetTourAgencyByIdQuery(Guid UserId) : IRequest<Result<TourAgencyDto>>;

public class GetTourAgencyByIdQueryHandler : IRequestHandler<GetTourAgencyByIdQuery, Result<TourAgencyDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTourAgencyByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<TourAgencyDto>> Handle(GetTourAgencyByIdQuery request, CancellationToken cancellationToken)
    {
        var agency = await _context.TourAgencies
            .Include(x => x.TourAgencyType)
            .Include(x => x.Tours)
            .FirstOrDefaultAsync(x => x.UserId == request.UserId);

        if (agency == null)
            return Result.Failure<TourAgencyDto>(ErrorCodes.ResourceNotFound);

        return Result.Success(new TourAgencyDto(agency));
    }
}
