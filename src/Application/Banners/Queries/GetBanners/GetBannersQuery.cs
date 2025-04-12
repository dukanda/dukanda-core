using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Application.Banners.Dto;
using Microsoft.EntityFrameworkCore;

namespace DukandaCore.Application.Banners.Queries.GetBanners;

public record GetBannersQuery : IRequest<Result<List<BannerDto>>>
{
    public bool? IsActive { get; init; }
    public bool? IsFeatured { get; init; }
    public int? BannerTypeId { get; init; }
}

public class GetBannersQueryHandler : IRequestHandler<GetBannersQuery, Result<List<BannerDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetBannersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<BannerDto>>> Handle(GetBannersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Banners.AsQueryable();

        if (request.IsActive.HasValue)
            query = query.Where(b => b.IsActive == request.IsActive.Value);

        if (request.IsFeatured.HasValue)
            query = query.Where(b => b.IsFeatured == request.IsFeatured.Value);

        if (request.BannerTypeId.HasValue)
            query = query.Where(b => b.BannerTypeId == request.BannerTypeId.Value);

        var banners = await query
            .OrderBy(b => b.DisplayOrder)
            .ThenByDescending(b => b.Created)
            .ToListAsync(cancellationToken);

        return Result.Success(banners.Select(b => new BannerDto(b)).ToList());
    }
} 