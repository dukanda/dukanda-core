using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Application.News.Dto;
using Microsoft.AspNetCore.Http;

namespace DukandaCore.Application.News.Commands.UpdateNews;

public record UpdateNewsCommand : IRequest<Result<NewsDto>>
{
    public Guid Id { get; init; }
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string Content { get; init; } = null!;
    public IFormFile? CoverImage { get; init; }
    public string? Tags { get; init; }
}

public class UpdateNewsCommandHandler : IRequestHandler<UpdateNewsCommand, Result<NewsDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICloudinaryService _cloudinaryService;

    public UpdateNewsCommandHandler(
        IApplicationDbContext context,
        ICloudinaryService cloudinaryService)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<Result<NewsDto>> Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
    {
        var news = await _context.News.FindAsync(request.Id);
        if (news == null)
            return Result.Failure<NewsDto>(ErrorCodes.ResourceNotFound);

        if (request.CoverImage != null)
        {
            await using var stream = request.CoverImage.OpenReadStream();
            news.CoverImageUrl = await _cloudinaryService.UploadFileAsync(stream, request.CoverImage.FileName);
        }

        news.Title = request.Title;
        news.Description = request.Description;
        news.Content = request.Content;
        news.Tags = request.Tags;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(new NewsDto(news));
    }
} 