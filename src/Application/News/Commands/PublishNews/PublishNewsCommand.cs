using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Application.News.Dto;

namespace DukandaCore.Application.News.Commands.PublishNews;

public record PublishNewsCommand : IRequest<Result<NewsDto>>
{
    public Guid Id { get; init; }
}

public class PublishNewsCommandHandler : IRequestHandler<PublishNewsCommand, Result<NewsDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _currentUserService;

    public PublishNewsCommandHandler(
        IApplicationDbContext context,
        IUser currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<NewsDto>> Handle(PublishNewsCommand request, CancellationToken cancellationToken)
    {
        var news = await _context.News.FindAsync(request.Id);
        if (news == null)
            return Result.Failure<NewsDto>(ErrorCodes.ResourceNotFound);

        news.PublishedAt = DateTimeOffset.UtcNow;
        news.PublishedById = _currentUserService.Id;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(new NewsDto(news));
    }
} 