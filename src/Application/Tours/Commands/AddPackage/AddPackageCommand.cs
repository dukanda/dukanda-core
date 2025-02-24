using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Domain.Entities;

public record AddPackageCommand : IRequest<Result<PackageDto>>
{
    public Guid TourId { get; init; }
    public string Name { get; init; } = null!;
    public decimal Price { get; init; }
    public List<BenefitCreateDto> Benefits { get; init; } = new();
}

public class AddPackageCommandHandler : IRequestHandler<AddPackageCommand, Result<PackageDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddPackageCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PackageDto>> Handle(AddPackageCommand request, CancellationToken cancellationToken)
    {
        var tour = await _context.Tours.FindAsync(request.TourId);
        if (tour == null)
            return Result.Failure<PackageDto>(ErrorCodes.ResourceNotFound);

        var package = new Package
        {
            TourId = request.TourId,
            Name = request.Name,
            Price = request.Price,
            Benefits = request.Benefits.Select(b => new Benefit
            {
                Name = b.Name,
                Description = b.Description
            }).ToList()
        };

        _context.Packages.Add(package);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(_mapper.Map<PackageDto>(package));
    }
}
