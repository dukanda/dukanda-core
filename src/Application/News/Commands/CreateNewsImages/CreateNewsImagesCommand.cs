using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace DukandaCore.Application.News.Commands.CreateNewsImages;

public record CreateNewsImagesCommand : IRequest<Result>
{
    public Guid NewsId { get; init; }
    public IFormFile Image { get; init; } = null!;
    public string? Caption { get; init; } = string.Empty;
    public int DisplayOrder { get; init; }
}

public class CreateNewsImagesCommandHandler : IRequestHandler<CreateNewsImagesCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ICloudinaryService _cloudinaryService;

    public CreateNewsImagesCommandHandler(
        IApplicationDbContext context,
        ICloudinaryService cloudinaryService)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<Result> Handle(CreateNewsImagesCommand request, CancellationToken cancellationToken)
    {
        var news = await _context.News.FindAsync(request.NewsId);
        if (news == null)
            return Result.Failure(ErrorCodes.ResourceNotFound);

        await using var stream = request.Image.OpenReadStream();
        var imageUrl = await _cloudinaryService.UploadFileAsync(stream, request.Image.FileName);
            
        var image = new NewsGallery
        {
            NewsId = request.NewsId,
            ImageUrl = imageUrl,
            Caption = request.Caption!,
            DisplayOrder = request.DisplayOrder,
        };
            
        _context.NewsGalleries.Add(image);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
} 