namespace DukandaCore.Domain.Entities;

public class TourItinerary : BaseAuditableEntity
{
    public Guid TourId { get; set; }
    public DateTime Date { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int DisplayOrder { get; set; }
    public Tour Tour { get; set; } = null!;
}