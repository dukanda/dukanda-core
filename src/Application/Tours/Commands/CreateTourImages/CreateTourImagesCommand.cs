using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace DukandaCore.Application.Tours.Commands.CreateTourImages;

public record CreateTourImagesCommand : IRequest<Result>
{
    public Guid TourId { get; init; }
    public IFormFile Image { get; init; } = null!;
    public string? Caption { get; init; } = string.Empty;
    public int DisplayOrder { get; init; }
}

public class CreateTourImagesCommandHandler : IRequestHandler<CreateTourImagesCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IMapper _mapper;

    public CreateTourImagesCommandHandler(
        IApplicationDbContext context,
        ICloudinaryService cloudinaryService,
        IMapper mapper)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
        _mapper = mapper;
    }

    public async Task<Result> Handle(CreateTourImagesCommand request, CancellationToken cancellationToken)
    {
        var tour = await _context.Tours.FindAsync(request.TourId);
        if (tour == null)
            return Result.Failure(ErrorCodes.ResourceNotFound);

        await using var stream = request.Image.OpenReadStream();
        var imageUrl = await _cloudinaryService.UploadFileAsync(stream, request.Image.FileName);
            
        var image = new TourGallery
        {
            TourId = request.TourId,
            ImageUrl = imageUrl,
            Caption = request.Caption!,
            DisplayOrder = request.DisplayOrder,
        };
            
        _context.TourGalleries.Add(image);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
} 