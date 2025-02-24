using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Domain.Entities;
using Microsoft.AspNetCore.Http;

public record CreateTourAgencyCommand : IRequest<Result<TourAgencyDto>>
{
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string ContactEmail { get; init; } = null!;
    public string ContactPhone { get; init; } = null!;
    public string Address { get; init; } = null!;
    public IFormFile Logo { get; init; } = null!;
    public int TourAgencyTypeId { get; init; }
}

public class CreateTourAgencyCommandHandler : IRequestHandler<CreateTourAgencyCommand, Result<TourAgencyDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IMapper _mapper;
    private readonly IUser _user;

    public CreateTourAgencyCommandHandler(
        IApplicationDbContext context,
        ICloudinaryService cloudinaryService,
        IMapper mapper,
        IUser user)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
        _mapper = mapper;
        _user = user;
    }

    public async Task<Result<TourAgencyDto>> Handle(CreateTourAgencyCommand request,
        CancellationToken cancellationToken)
    {
        using var stream = request.Logo.OpenReadStream();
        var logoUrl = await _cloudinaryService.UploadFileAsync(stream, request.Logo.FileName);

        var agency = new TourAgency
        {
            Name = request.Name,
            Description = request.Description,
            ContactEmail = request.ContactEmail,
            ContactPhone = request.ContactPhone,
            Address = request.Address,
            LogoUrl = logoUrl,
            TourAgencyTypeId = request.TourAgencyTypeId,
            IsVerified = false,
            UserId = _user.Id!.Value,
        };

        _context.TourAgencies.Add(agency);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(new TourAgencyDto(agency));
    }
}