using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Application.Tours.Dtos;
using DukandaCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DukandaCore.Application.Tours.Commands.UpdatePackage;

public record UpdatePackageCommand : IRequest<Result<PackageDto>>
{
    public Guid PackageId { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public List<BenefitUpdateDto> Benefits { get; init; } = new();
}

public class UpdatePackageCommandHandler : IRequestHandler<UpdatePackageCommand, Result<PackageDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdatePackageCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PackageDto>> Handle(UpdatePackageCommand request, CancellationToken cancellationToken)
    {
        var package = await _context.Packages
            .Include(p => p.Benefits)
            .FirstOrDefaultAsync(p => p.Id == request.PackageId, cancellationToken);

        if (package == null)
            return Result.Failure<PackageDto>(ErrorCodes.ResourceNotFound);

        // Update basic properties
        package.Name = request.Name;
        package.Price = request.Price;

        // Update benefits
        var existingBenefits = package.Benefits.ToList();
        var updatedBenefits = request.Benefits;

        // Remove benefits that are not in the update request
        var benefitsToRemove = existingBenefits
            .Where(eb => !updatedBenefits.Any(ub => ub.Id == eb.Id))
            .ToList();

        foreach (var benefit in benefitsToRemove)
        {
            package.Benefits.Remove(benefit);
        }

        // Update existing and add new benefits
        foreach (var benefitDto in updatedBenefits)
        {
            if (benefitDto.Id.HasValue)
            {
                // Update existing benefit
                var existingBenefit = existingBenefits.FirstOrDefault(b => b.Id == benefitDto.Id);
                if (existingBenefit != null)
                { 
                    
                    existingBenefit.Name = benefitDto.Name;
                    existingBenefit.Description = benefitDto.Description;
                }
            }
            else
            {
                package.Benefits.Add(new Benefit
                {
                    PackageId = package.Id,
                    Name = benefitDto.Name,
                    Description = benefitDto.Description,
                });
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(_mapper.Map<PackageDto>(package));
    }
}
