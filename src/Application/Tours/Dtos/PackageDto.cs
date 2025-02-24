public class PackageDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public Guid TourId { get; set; }
    public List<BenefitDto> Benefits { get; set; } = new();
    public int BookingsCount { get; set; }
}