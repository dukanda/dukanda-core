using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Domain.Entities;
using Microsoft.AspNetCore.Http;

public record CreateTourCommand : IRequest<Result<TourDto>>
{
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public decimal BasePrice { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int CityId { get; init; }
    public IFormFile Cover { get; init; } = null!;

    public List<int> TourTypeIds { get; init; } = new();
}

public class CreateTourCommandHandler : IRequestHandler<CreateTourCommand, Result<TourDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICloudinaryService _cloudinaryService;
   private readonly IUser _currentUser;
    public CreateTourCommandHandler(IApplicationDbContext context, 
        ICloudinaryService cloudinaryService,
        IUser currentUser)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
        _currentUser = currentUser;
    }

    public async Task<Result<TourDto>> Handle(CreateTourCommand request, CancellationToken cancellationToken)
    {

        if (_currentUser.Id == null)
        {
            return Result.Failure<TourDto>("You are not logged in!");
        }
        
        // Upload cover image
        string coverUrl;
        using (var stream = request.Cover.OpenReadStream())
        {
            coverUrl = await _cloudinaryService.UploadFileAsync(stream, $"tours/{Guid.NewGuid()}_{request.Cover.FileName}");
        }

        var tourTypes = await _context.TourTypes
            .Where(tt => request.TourTypeIds.Contains(tt.Id))
            .ToListAsync(cancellationToken);

        var tour = new Tour
        {
            AgencyId = _currentUser.Id.Value,
            Title = request.Title,
            Description = request.Description,
            BasePrice = request.BasePrice,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CityId = request.CityId,
            CoverImageUrl = coverUrl,
            TourTypes = tourTypes
        };

        _context.Tours.Add(tour);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(new TourDto(tour));
    }
}