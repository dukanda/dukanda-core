using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Domain.Entities;
using Microsoft.AspNetCore.Http;

public record CreateTouristAttractionCommand : IRequest<Result<TouristAttractionDto>>
{
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string Location { get; init; } = null!;
    public int CityId { get; init; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public IFormFile Image { get; init; } = null!;
}

public class CreateTouristAttractionCommandHandler : IRequestHandler<CreateTouristAttractionCommand, Result<TouristAttractionDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICloudinaryService _cloudinaryService;

    public CreateTouristAttractionCommandHandler(IApplicationDbContext context, ICloudinaryService cloudinaryService)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<Result<TouristAttractionDto>> Handle(CreateTouristAttractionCommand request, CancellationToken cancellationToken)
    {
        using var stream = request.Image.OpenReadStream();
        var imageUrl = await _cloudinaryService.UploadFileAsync(stream, request.Image.FileName);

        var attraction = new TouristAttraction
        {
            Name = request.Name,
            Description = request.Description,
            CityId = request.CityId,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            ImageUrl = imageUrl
        };

        _context.TouristAttractions.Add(attraction);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(new TouristAttractionDto(attraction));
    }
}
