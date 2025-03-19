using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Application.News.Dto;

namespace DukandaCore.Application.News.Queries.GetNews;

public record GetNewsQuery : IRequest<Result<List<NewsDto>>>
{
    public string? SearchTerm { get; init; }
    public string? Tag { get; init; }
    internal bool IncludeUnpublished { get;  set; }

    public void IncludeUnpublishedNews()
    {
        IncludeUnpublished = true;
    }
}

public class GetNewsQueryHandler : IRequestHandler<GetNewsQuery, Result<List<NewsDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetNewsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<NewsDto>>> Handle(GetNewsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.News
            .Include(n => n.PublishedBy)
            .AsQueryable();

        if (!request.IncludeUnpublished)
            query = query.Where(n => n.PublishedAt.HasValue);

        if (!string.IsNullOrEmpty(request.SearchTerm))
            query = query.Where(n => n.Title.Contains(request.SearchTerm) || 
                                   n.Description.Contains(request.SearchTerm));

        if (!string.IsNullOrEmpty(request.Tag))
            query = query.Where(n => n.Tags != null && n.Tags.Contains(request.Tag));

        var news = await query
            .OrderByDescending(n => n.PublishedAt)
            .ThenByDescending(n => n.Created)
            .ToListAsync(cancellationToken);

        return Result.Success(news.Select(n => new NewsDto(n)).ToList());
    }
} 