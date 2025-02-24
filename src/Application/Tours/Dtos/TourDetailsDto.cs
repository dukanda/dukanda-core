using DukandaCore.Domain.Entities;

public class TourDetailDto: TourDto
{
   
    public List<TourItineraryDto> Itineraries { get; }
    public List<TouristAttractionDto> Attractions { get; }

    public TourDetailDto(Tour tour): base(tour)
    {
        Itineraries = tour.Itineraries.OrderBy(i => i.DisplayOrder).Select(i => new TourItineraryDto(i)).ToList();
        Attractions = tour.Attractions.Select(a => new TouristAttractionDto(a)).ToList();
    }
}
