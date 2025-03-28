using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Application.News.Dto;

namespace DukandaCore.Application.News.Commands.SetNewsFeatured;

public record SetNewsFeaturedCommand : IRequest<Result<NewsDto>>
{
    public Guid Id { get; init; }
    public bool IsFeatured { get; init; }
}

public class SetNewsFeaturedCommandHandler : IRequestHandler<SetNewsFeaturedCommand, Result<NewsDto>>
{
    private readonly IApplicationDbContext _context;

    public SetNewsFeaturedCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<NewsDto>> Handle(SetNewsFeaturedCommand request, CancellationToken cancellationToken)
    {
        var news = await _context.News.FindAsync(request.Id);
        if (news == null)
            return Result.Failure<NewsDto>(ErrorCodes.ResourceNotFound);

        news.IsFeatured = request.IsFeatured;
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(new NewsDto(news));
    }
} 