using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;

public record DeleteTouristAttractionCommand(Guid Id) : IRequest<Result>;

public class DeleteTouristAttractionCommandHandler : IRequestHandler<DeleteTouristAttractionCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ICloudinaryService _cloudinaryService;

    public DeleteTouristAttractionCommandHandler(IApplicationDbContext context, ICloudinaryService cloudinaryService)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<Result> Handle(DeleteTouristAttractionCommand request, CancellationToken cancellationToken)
    {
        var attraction = await _context.TouristAttractions.FindAsync(request.Id);

        if (attraction == null)
        {
            return Result.Failure(ErrorCodes.ResourceNotFound);
        }

        if (!string.IsNullOrEmpty(attraction.ImageUrl))
        {
            await _cloudinaryService.DeleteFileAsync(attraction.ImageUrl);
        }

        _context.TouristAttractions.Remove(attraction);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
