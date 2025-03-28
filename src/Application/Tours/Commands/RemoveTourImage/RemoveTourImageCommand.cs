using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;

namespace DukandaCore.Application.Tours.Commands.RemoveTourImage;

public record RemoveTourImageCommand : IRequest<Result>
{
    public Guid TourId { get; init; }
    public Guid ImageId { get; init; }
}

public class RemoveTourImageCommandHandler : IRequestHandler<RemoveTourImageCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public RemoveTourImageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(RemoveTourImageCommand request, CancellationToken cancellationToken)
    {
        var image = await _context.TourGalleries
            .FirstOrDefaultAsync(x => x.Id == request.ImageId && x.TourId == request.TourId);

        if (image == null)
            return Result.Failure(ErrorCodes.ResourceNotFound);

        _context.TourGalleries.Remove(image);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
} 