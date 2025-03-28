using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Application.Tours.Dtos;
using DukandaCore.Domain.Entities;

namespace DukandaCore.Application.Tours.Commands.AddPackage;

public class AddPackageCommand : IRequest<Result<PackageDto>>
{
    public Guid TourId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public required List<BenefitCreateDto> Benefits { get; set; }
}

public class AddPackageCommandHandler : IRequestHandler<AddPackageCommand, Result<PackageDto>>
{
    private readonly IApplicationDbContext _context;

    public AddPackageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
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

        return Result.Success(new PackageDto(package));
    }
}
