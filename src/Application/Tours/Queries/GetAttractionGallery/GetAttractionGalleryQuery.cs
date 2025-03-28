using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using Microsoft.Extensions.Logging;

public record GetAttractionGalleryQuery : IRequest<Result<List<AttractionGalleryDto>>>
{
    public Guid TourId { get; init; }
    public Guid AttractionId { get; init; }
}

public class GetAttractionGalleryQueryHandler : IRequestHandler<GetAttractionGalleryQuery, Result<List<AttractionGalleryDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetAttractionGalleryQueryHandler> _logger;

    public GetAttractionGalleryQueryHandler(
        IApplicationDbContext context, 
        ILogger<GetAttractionGalleryQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<List<AttractionGalleryDto>>> Handle(GetAttractionGalleryQuery request, CancellationToken cancellationToken)
    {

            // Validate tour and attraction relationship
            var tourAttraction = await _context.Tours
                .Include(t => t.Attractions)
                .FirstOrDefaultAsync(t => 
                    t.Id == request.TourId && 
                    t.Attractions.Any(a => a.Id == request.AttractionId), 
                cancellationToken);

            if (tourAttraction == null)
            {
                return Result.Failure<List<AttractionGalleryDto>>(ErrorCodes.ResourceNotFound);
            }

            var gallery = await _context.TouristAttractions
                .Include(a => a.Gallery)
                .Where(a => a.Id == request.AttractionId)
                .SelectMany(a => a.Gallery)
                .OrderBy(g => g.DisplayOrder)
                .Select(g => new AttractionGalleryDto(g))
                .ToListAsync(cancellationToken);
            

            return Result.Success(gallery);
    }
}
