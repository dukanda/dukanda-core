using DukandaCore.Application.Common.Models;

public record UpdatePackageCommand : IRequest<Result<PackageDto>>
{
    public Guid PackageId { get; init; }
    public string Name { get; init; } = null!;
    public decimal Price { get; init; }
    public List<BenefitUpdateDto> Benefits { get; init; } = new();
}
