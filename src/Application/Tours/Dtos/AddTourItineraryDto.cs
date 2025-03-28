public record AddTourItineraryDto
{
    public DateTime Date { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int DisplayOrder { get; set; }
}