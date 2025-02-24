using DukandaCore.Domain.Entities;

public class PackageDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public Guid TourId { get; set; }
    public List<BenefitDto> Benefits { get; set; } = new();

    public PackageDto(Package package)
    {
        Id = package.Id;
        Name = package.Name;
        Price = package.Price;
        TourId = package.TourId;
        Benefits = package.Benefits?.Select(x => new BenefitDto(x))?.ToList() ?? new();
    }
}