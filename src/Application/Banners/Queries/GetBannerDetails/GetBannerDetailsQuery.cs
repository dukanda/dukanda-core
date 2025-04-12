using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Application.Banners.Dto;
using Microsoft.EntityFrameworkCore;

namespace DukandaCore.Application.Banners.Queries.GetBannerDetails;

public record GetBannerDetailsQuery : IRequest<Result<BannerDto>>
{
    public Guid Id { get; init; }
}

public class GetBannerDetailsQueryHandler : IRequestHandler<GetBannerDetailsQuery, Result<BannerDto>>
{
    private readonly IApplicationDbContext _context;

    public GetBannerDetailsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<BannerDto>> Handle(GetBannerDetailsQuery request, CancellationToken cancellationToken)
    {
        var banner = await _context.Banners
            .Include(b => b.BannerType)
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (banner == null)
            return Result.Failure<BannerDto>("Banner not found");

        return Result.Success(new BannerDto(banner));
    }
} 