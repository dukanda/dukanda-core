using DukandaCore.Domain.Entities;

public record TouristAttractionDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string ImageUrl { get; init; } = null!;
    public bool IsFeatured { get; init; }
    public int CityId { get; init; }
    public string CityName { get; init; } = null!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public TouristAttractionDto(TouristAttraction attraction)
    {
        Id = attraction.Id;
        Name = attraction.Name;
        Description = attraction.Description;
       Latitude = attraction.Latitude;
       Longitude = attraction.Longitude;
        ImageUrl = attraction.ImageUrl;
        IsFeatured = attraction.IsFeatured;
        CityId = attraction.CityId;
        CityName = attraction.City?.Name ?? "";
    }
}