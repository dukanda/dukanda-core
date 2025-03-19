using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Application.News.Dto;

namespace DukandaCore.Application.News.Queries.GetFeaturedNews;

public record GetFeaturedNewsQuery : IRequest<Result<List<NewsDto>>>
{
}

public class GetFeaturedNewsQueryHandler : IRequestHandler<GetFeaturedNewsQuery, Result<List<NewsDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetFeaturedNewsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<NewsDto>>> Handle(GetFeaturedNewsQuery request, CancellationToken cancellationToken)
    {
        var news = await _context.News
            .Include(n => n.PublishedBy)
            .Where(n => n.IsFeatured && n.PublishedAt.HasValue)
            .OrderByDescending(n => n.PublishedAt)
            .ToListAsync(cancellationToken);

        return Result.Success(news.Select(n => new NewsDto(n)).ToList());
    }
} 