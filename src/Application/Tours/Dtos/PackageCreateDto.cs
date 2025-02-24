public class PackageCreateDto
{
    public string Name { get; init; } = null!;
    public decimal Price { get; init; }
    public List<BenefitCreateDto> Benefits { get; init; } = new();
}