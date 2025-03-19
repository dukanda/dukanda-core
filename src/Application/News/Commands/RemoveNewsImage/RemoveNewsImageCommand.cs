using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;

namespace DukandaCore.Application.News.Commands.RemoveNewsImage;

public record RemoveNewsImageCommand : IRequest<Result>
{
    public Guid NewsId { get; init; }
    public Guid ImageId { get; init; }
}

public class RemoveNewsImageCommandHandler : IRequestHandler<RemoveNewsImageCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public RemoveNewsImageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(RemoveNewsImageCommand request, CancellationToken cancellationToken)
    {
        var image = await _context.NewsGalleries
            .FirstOrDefaultAsync(x => x.Id == request.ImageId && x.NewsId == request.NewsId);

        if (image == null)
            return Result.Failure(ErrorCodes.ResourceNotFound);

        _context.NewsGalleries.Remove(image);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
} 