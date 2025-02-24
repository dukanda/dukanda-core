public class BookingDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid PackageId { get; set; }
    public int BookingStatusId { get; set; }
    public decimal Price { get; set; }
    public string Status { get; set; } = null!;
    public PackageDto Package { get; set; } = null!;
    public DateTimeOffset Created { get; set; }
}
