using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Application.Banners.Dto;
using Microsoft.AspNetCore.Http;

namespace DukandaCore.Application.Banners.Commands.CreateBanner;

public record CreateBannerCommand : IRequest<Result<BannerDto>>
{
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public IFormFile Image { get; init; } = null!;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public bool IsActive { get; init; }
    public bool IsFeatured { get; init; }
    public string LinkUrl { get; init; } = null!;
    public int BannerTypeId { get; init; }
    public int DisplayOrder { get; init; }
}

public class CreateBannerCommandHandler : IRequestHandler<CreateBannerCommand, Result<BannerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICloudinaryService _cloudinaryService;

    public CreateBannerCommandHandler(
        IApplicationDbContext context,
        ICloudinaryService cloudinaryService)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<Result<BannerDto>> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        await using var stream = request.Image.OpenReadStream();
        var imageUrl = await _cloudinaryService.UploadFileAsync(stream, request.Image.FileName);

        var banner = new Domain.Entities.Banner
        {
            Title = request.Title,
            Description = request.Description,
            ImageUrl = imageUrl,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            IsActive = request.IsActive,
            IsFeatured = request.IsFeatured,
            LinkUrl = request.LinkUrl,
            BannerTypeId = request.BannerTypeId,
            DisplayOrder = request.DisplayOrder
        };

        _context.Banners.Add(banner);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(new BannerDto(banner));
    }
} 