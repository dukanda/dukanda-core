using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Application.News.Dto;

namespace DukandaCore.Application.News.Queries.GetNewsDetails;

public record GetNewsDetailsQuery : IRequest<Result<NewsDetailDto>>
{
    public Guid NewsId { get; init; }
}

public class GetNewsDetailsQueryHandler : IRequestHandler<GetNewsDetailsQuery, Result<NewsDetailDto>>
{
    private readonly IApplicationDbContext _context;

    public GetNewsDetailsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<NewsDetailDto>> Handle(GetNewsDetailsQuery request, CancellationToken cancellationToken)
    {
        var news = await _context.News
            .Include(n => n.PublishedBy)
            .Include(n => n.Gallery)
            .FirstOrDefaultAsync(n => n.Id == request.NewsId, cancellationToken);

        if (news == null)
            return Result.Failure<NewsDetailDto>(ErrorCodes.ResourceNotFound);

        return Result.Success(new NewsDetailDto(news));
    }
} 