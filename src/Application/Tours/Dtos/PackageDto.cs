using DukandaCore.Domain.Entities;

namespace DukandaCore.Application.Tours.Dtos;

public record PackageDto
{
    public Guid Id { get; init; }
    public Guid TourId { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public List<BenefitDto> Benefits { get; init; } = new();

    public PackageDto(Package package)
    {
        Id = package.Id;
        TourId = package.TourId;
        Name = package.Name;
        Price = package.Price;
        Benefits = package.Benefits.Select(b => new BenefitDto(b)).ToList();
    }
}