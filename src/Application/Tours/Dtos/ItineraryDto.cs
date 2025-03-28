using DukandaCore.Domain.Entities;

public class TourItineraryDto
{
    public Guid Id { get; set; }
    public Guid TourId { get; set; }
    public DateTime Date { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int DisplayOrder { get; set; }

    public TourItineraryDto()
    {
    }

    public TourItineraryDto(TourItinerary tourItinerary)
    {
        Id = tourItinerary.Id;
        TourId = tourItinerary.TourId;
        Date = tourItinerary.Date;
        Title = tourItinerary.Title;
        Description = tourItinerary.Description;
        DisplayOrder = tourItinerary.DisplayOrder;
    }
}