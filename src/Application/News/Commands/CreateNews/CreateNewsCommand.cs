using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Application.News.Dto;
using Microsoft.AspNetCore.Http;

namespace DukandaCore.Application.News.Commands.CreateNews;

public record CreateNewsCommand : IRequest<Result<NewsDto>>
{
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string Content { get; init; } = null!;
    public IFormFile CoverImage { get; init; } = null!;
    public string? Tags { get; init; }
}

public class CreateNewsCommandHandler : IRequestHandler<CreateNewsCommand, Result<NewsDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IMapper _mapper;

    public CreateNewsCommandHandler(
        IApplicationDbContext context,
        ICloudinaryService cloudinaryService,
        IMapper mapper)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
        _mapper = mapper;
    }

    public async Task<Result<NewsDto>> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
    {
        await using var stream = request.CoverImage.OpenReadStream();
        var imageUrl = await _cloudinaryService.UploadFileAsync(stream, request.CoverImage.FileName);

        var news = new Domain.Entities.News
        {
            Title = request.Title,
            Description = request.Description,
            Content = request.Content,
            CoverImageUrl = imageUrl,
            Tags = request.Tags
        };

        _context.News.Add(news);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(new NewsDto(news));
    }
} 