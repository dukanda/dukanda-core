using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;

public record GetTouristAttractionByIdQuery(Guid Id) : IRequest<Result<TouristAttractionDto>>;

public class GetTouristAttractionByIdQueryHandler : IRequestHandler<GetTouristAttractionByIdQuery, Result<TouristAttractionDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTouristAttractionByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<TouristAttractionDto>> Handle(GetTouristAttractionByIdQuery request, CancellationToken cancellationToken)
    {
        var attraction = await _context.TouristAttractions
            .Include(t => t.City)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (attraction == null)
        {
            return Result.Failure<TouristAttractionDto>(ErrorCodes.ResourceNotFound);
        }

        return Result<TouristAttractionDto>.Success(new TouristAttractionDto(attraction));
    }
}
