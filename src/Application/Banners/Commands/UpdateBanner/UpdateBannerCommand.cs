using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Application.Banners.Dto;
using Microsoft.AspNetCore.Http;

namespace DukandaCore.Application.Banners.Commands.UpdateBanner;

public record UpdateBannerCommand : IRequest<Result<BannerDto>>
{
    public Guid Id { get; init; }
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public IFormFile? Image { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public bool IsActive { get; init; }
    public bool IsFeatured { get; init; }
    public string LinkUrl { get; init; } = null!;
    public int BannerTypeId { get; init; }
    public int DisplayOrder { get; init; }
}

public class UpdateBannerCommandHandler : IRequestHandler<UpdateBannerCommand, Result<BannerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICloudinaryService _cloudinaryService;

    public UpdateBannerCommandHandler(
        IApplicationDbContext context,
        ICloudinaryService cloudinaryService)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<Result<BannerDto>> Handle(UpdateBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await _context.Banners.FindAsync(new object[] { request.Id }, cancellationToken);
        
        if (banner == null)
            return Result.Failure<BannerDto>("Banner not found");

        if (request.Image != null)
        {
            await using var stream = request.Image.OpenReadStream();
            var imageUrl = await _cloudinaryService.UploadFileAsync(stream, request.Image.FileName);
            banner.ImageUrl = imageUrl;
        }

        banner.Title = request.Title;
        banner.Description = request.Description;
        banner.StartDate = request.StartDate;
        banner.EndDate = request.EndDate;
        banner.IsActive = request.IsActive;
        banner.IsFeatured = request.IsFeatured;
        banner.LinkUrl = request.LinkUrl;
        banner.BannerTypeId = request.BannerTypeId;
        banner.DisplayOrder = request.DisplayOrder;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(new BannerDto(banner));
    }
} 