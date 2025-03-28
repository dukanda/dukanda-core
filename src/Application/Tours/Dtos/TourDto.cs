using DukandaCore.Domain.Entities;

public class TourDto
{
    public Guid Id { get; }
    public string Title { get; }
    public string Description { get; }
    public decimal BasePrice { get; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
    public string AgencyName { get; }
    public string AgencyLogoUrl { get; set; }
    public string CityName { get; set; } = null!;
    public string CoverImageUrl { get; set; } = null!;
    public List<TourTypeDto> TourTypes { get; }
    public DateTimeOffset Created { get; }

    public TourDto(Tour tour)
    {
        Id = tour.Id;
        Title = tour.Title;
        CoverImageUrl = tour.CoverImageUrl;
        Description = tour.Description;
        BasePrice = tour.BasePrice;
        StartDate = tour.StartDate;
        EndDate = tour.EndDate;
        AgencyName = tour.Agency?.Name ?? "";
        CityName = tour.City?.Name ?? string.Empty;
        AgencyLogoUrl = tour.Agency?.LogoUrl ?? string.Empty;
        TourTypes = tour.TourTypes?.Select(tt => new TourTypeDto(tt))?.ToList() ?? new();
        Created = tour.Created;
    }

 
}

public class TourTypeDto
{
    public int Id { get; }
    public string Name { get; }
    public string Icon { get; }

    public TourTypeDto(TourType tourType)
    {
        Id = tourType.Id;
        Name = tourType.Name;
        Icon = tourType.Icon;
    }
}