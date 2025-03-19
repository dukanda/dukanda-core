namespace DukandaCore.Application.Tours.Dto;

public record TourAvailabilityDto
{
    public Guid TourId { get; init; }
    public DateTime Date { get; init; }
    public bool IsAvailable { get; init; }
    public int AvailableSpots { get; init; }
    public decimal Price { get; init; }
    public List<string> BlockingReasons { get; init; } = new();

    public TourAvailabilityDto(Guid tourId, DateTime date, bool isAvailable, int availableSpots, decimal price)
    {
        TourId = tourId;
        Date = date;
        IsAvailable = isAvailable;
        AvailableSpots = availableSpots;
        Price = price;
    }
} 