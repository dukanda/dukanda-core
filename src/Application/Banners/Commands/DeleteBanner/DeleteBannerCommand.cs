using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;

namespace DukandaCore.Application.Banners.Commands.DeleteBanner;

public record DeleteBannerCommand : IRequest<Result>
{
    public Guid Id { get; init; }
}

public class DeleteBannerCommandHandler : IRequestHandler<DeleteBannerCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public DeleteBannerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await _context.Banners.FindAsync(new object[] { request.Id }, cancellationToken);
        
        if (banner == null)
            return Result.Failure("Banner not found");

        _context.Banners.Remove(banner);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
} 