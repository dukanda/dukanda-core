using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Domain.Entities;
using Microsoft.AspNetCore.Http;

public record CreateAttractionImagesCommand : IRequest<Result>
{
    public Guid TouristAttractionId { get; init; }
    public IFormFile Image { get; init; } = null!;
    public string? Caption { get; init; } = string.Empty;
    public int DisplayOrder { get; init; }
}

public class CreateAttractionImagesCommandHandler : IRequestHandler<CreateAttractionImagesCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IMapper _mapper;

    public CreateAttractionImagesCommandHandler(
        IApplicationDbContext context,
        ICloudinaryService cloudinaryService,
        IMapper mapper)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
        _mapper = mapper;
    }

    public async Task<Result> Handle(CreateAttractionImagesCommand request, CancellationToken cancellationToken)
    {
        var attraction = await _context.TouristAttractions.FindAsync(request.TouristAttractionId);
        if (attraction == null)
            return Result.Failure<List<AttractionImageDto>>(ErrorCodes.ResourceNotFound);


        await using var stream = request.Image.OpenReadStream();
        var imageUrl = await _cloudinaryService.UploadFileAsync(stream, request.Image.FileName);
            
        var image = new AttractionImage
        {
            TouristAttractionId = request.TouristAttractionId,
            ImageUrl = imageUrl,
            Caption = request.Caption!,
            DisplayOrder = request.DisplayOrder,
        };
            
        _context.AttractionImages.Add(image);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
