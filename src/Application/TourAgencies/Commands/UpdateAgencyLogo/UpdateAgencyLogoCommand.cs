using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using Microsoft.AspNetCore.Http;

public record UpdateAgencyLogoCommand : IRequest<Result<string>>
{
    public Guid UserId { get; init; }
    public IFormFile Logo { get; init; } = null!;
}

public class UpdateAgencyLogoCommandHandler : IRequestHandler<UpdateAgencyLogoCommand, Result<string>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICloudinaryService _cloudinaryService;

    public UpdateAgencyLogoCommandHandler(IApplicationDbContext context, ICloudinaryService cloudinaryService)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<Result<string>> Handle(UpdateAgencyLogoCommand request, CancellationToken cancellationToken)
    {
        var agency = await _context.TourAgencies.FindAsync(request.UserId);
        if (agency == null)
            return Result.Failure<string>(ErrorCodes.ResourceNotFound);

        using var stream = request.Logo.OpenReadStream();
        var logoUrl = await _cloudinaryService.UploadFileAsync(stream, request.Logo.FileName);

        agency.LogoUrl = logoUrl;
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(logoUrl);
    }
}
