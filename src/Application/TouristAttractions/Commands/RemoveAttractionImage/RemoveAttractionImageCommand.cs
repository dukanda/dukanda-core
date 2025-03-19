using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace DukandaCore.Application.TouristAttractions.Commands.RemoveAttractionImage;

public record RemoveAttractionImageCommand : IRequest<Result>
{
    public Guid AttractionId { get; init; }
    public Guid ImageId { get; init; }
}

public class RemoveAttractionImageCommandHandler : IRequestHandler<RemoveAttractionImageCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ICloudinaryService _cloudinaryService;

    public RemoveAttractionImageCommandHandler(
        IApplicationDbContext context,
        ICloudinaryService cloudinaryService)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<Result> Handle(RemoveAttractionImageCommand request, CancellationToken cancellationToken)
    {
        var image = await _context.AttractionGalleries
            .FirstOrDefaultAsync(i => i.Id == request.ImageId && i.TouristAttractionId == request.AttractionId, 
                cancellationToken);

        if (image == null)
            return Result.Failure(ErrorCodes.ResourceNotFound);

        // Delete from Cloudinary
        if (!string.IsNullOrEmpty(image.ImageUrl))
        {
            await _cloudinaryService.DeleteFileAsync(image.ImageUrl);
        }

        _context.AttractionGalleries.Remove(image);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
} 