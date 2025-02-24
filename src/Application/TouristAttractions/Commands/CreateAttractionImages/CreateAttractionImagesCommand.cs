using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Domain.Entities;
using Microsoft.AspNetCore.Http;

public class AttractionImageCreateDto
{
    public IFormFile Image { get; init; } = null!;
    public string Caption { get; init; } = string.Empty;
    public int DisplayOrder { get; init; }
}

public record CreateAttractionImagesCommand : IRequest<Result<List<AttractionImageDto>>>
{
    public Guid TouristAttractionId { get; init; }
    public List<AttractionImageCreateDto> Images { get; init; } = new();
}

public class CreateAttractionImagesCommandHandler : IRequestHandler<CreateAttractionImagesCommand, Result<List<AttractionImageDto>>>
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

    public async Task<Result<List<AttractionImageDto>>> Handle(CreateAttractionImagesCommand request, CancellationToken cancellationToken)
    {
        var attraction = await _context.TouristAttractions.FindAsync(request.TouristAttractionId);
        if (attraction == null)
            return Result.Failure<List<AttractionImageDto>>(ErrorCodes.ResourceNotFound);

        var images = new List<AttractionImage>();
        
        foreach (var imageDto in request.Images)
        {
            using var stream = imageDto.Image.OpenReadStream();
            var imageUrl = await _cloudinaryService.UploadFileAsync(stream, imageDto.Image.FileName);
            
            var image = new AttractionImage
            {
                TouristAttractionId = request.TouristAttractionId,
                ImageUrl = imageUrl,
                Caption = imageDto.Caption,
                DisplayOrder = imageDto.DisplayOrder
            };
            
            images.Add(image);
        }

        _context.AttractionImages.AddRange(images);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(_mapper.Map<List<AttractionImageDto>>(images));
    }
}
