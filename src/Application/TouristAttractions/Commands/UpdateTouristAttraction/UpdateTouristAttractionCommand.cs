using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using Microsoft.AspNetCore.Http;

public record UpdateTouristAttractionCommand : IRequest<Result<TouristAttractionDto>>
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string Location { get; init; } = null!;
    public int CityId { get; init; }
    public IFormFile? Image { get; init; }
}

public class UpdateTouristAttractionCommandHandler : IRequestHandler<UpdateTouristAttractionCommand, Result<TouristAttractionDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICloudinaryService _cloudinaryService;

    public UpdateTouristAttractionCommandHandler(IApplicationDbContext context, ICloudinaryService cloudinaryService)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<Result<TouristAttractionDto>> Handle(UpdateTouristAttractionCommand request, CancellationToken cancellationToken)
    {
        var attraction = await _context.TouristAttractions.FindAsync(request.Id);

        if (attraction == null)
        {
            return Result.Failure<TouristAttractionDto>(ErrorCodes.ResourceNotFound);
        }

        if (request.Image != null)
        {
            using var stream = request.Image.OpenReadStream();
            attraction.ImageUrl = await _cloudinaryService.UploadFileAsync(stream, request.Image.FileName);
        }

        attraction.Name = request.Name;
        attraction.Description = request.Description;
        attraction.Location = request.Location;
        attraction.CityId = request.CityId;

        await _context.SaveChangesAsync(cancellationToken);

        return Result<TouristAttractionDto>.Success(new TouristAttractionDto(attraction));
    }
}
