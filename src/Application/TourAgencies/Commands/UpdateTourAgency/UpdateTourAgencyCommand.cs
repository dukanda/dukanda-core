using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;

public record UpdateTourAgencyCommand : IRequest<Result<TourAgencyDto>>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string ContactEmail { get; init; } = null!;
    public string ContactPhone { get; init; } = null!;
    public string Address { get; init; } = null!;
    public int TourAgencyTypeId { get; init; }
}

public class UpdateTourAgencyCommandHandler : IRequestHandler<UpdateTourAgencyCommand, Result<TourAgencyDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateTourAgencyCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<TourAgencyDto>> Handle(UpdateTourAgencyCommand request, CancellationToken cancellationToken)
    {
        var agency = await _context.TourAgencies.FindAsync(request.UserId);
        if (agency == null)
            return Result.Failure<TourAgencyDto>(ErrorCodes.ResourceNotFound);

        agency.Name = request.Name;
        agency.Description = request.Description;
        agency.ContactEmail = request.ContactEmail;
        agency.ContactPhone = request.ContactPhone;
        agency.Address = request.Address;
        agency.TourAgencyTypeId = request.TourAgencyTypeId;

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(_mapper.Map<TourAgencyDto>(agency));
    }
}
