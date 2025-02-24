using DukandaCore.Domain.Entities;

public record TouristAttractionDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string Location { get; init; } = null!;
    public string ImageUrl { get; init; } = null!;
    public bool IsFeatured { get; init; }
    public string CityName { get; init; } = null!;

    public TouristAttractionDto(TouristAttraction attraction)
    {
        Id = attraction.Id;
        Name = attraction.Name;
        Description = attraction.Description;
        Location = attraction.Location;
        ImageUrl = attraction.ImageUrl;
        IsFeatured = attraction.IsFeatured;
        CityName = attraction.City?.Name ?? "";
    }
}
