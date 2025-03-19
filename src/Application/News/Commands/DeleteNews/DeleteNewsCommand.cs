using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;

namespace DukandaCore.Application.News.Commands.DeleteNews;

public record DeleteNewsCommand : IRequest<Result>
{
    public Guid Id { get; init; }
}

public class DeleteNewsCommandHandler : IRequestHandler<DeleteNewsCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public DeleteNewsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
    {
        var news = await _context.News.FindAsync(request.Id);
        if (news == null)
            return Result.Failure(ErrorCodes.ResourceNotFound);

        _context.News.Remove(news);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
} 