using DukandaCore.Domain.Entities;

public class BookingDetailsDto : BookingDto
{
    public int PaymentMethodId { get; set; }
    public DateTimeOffset? PaymentDate { get; set; }
    public string? QrCodeUrl { get; set; }
    public string? CancellationReason { get; set; }
    public DateTimeOffset? ExpirationDate { get; set; }
    public List<BookingItemDto> Items { get; set; } = new();

    public BookingDetailsDto(Booking booking) : base(booking)
    {
        PaymentMethodId = booking.PaymentIntent.PaymentMethodId;
        PaymentDate = booking.PaymentDate;
        QrCodeUrl = booking.QrCodeUrl;
        CancellationReason = booking.CancellationReason;
        ExpirationDate = booking.ExpirationDate;

        Items = booking.Items.Select(item => new BookingItemDto
        {
            Id = item.Id,
            PackageId = item.PackageId,
            Quantity = item.Quantity,
            UnitPrice = item.UnitPrice,
            TotalPrice = item.TotalPrice
        }).ToList();
    }
}

public class BookingItemDto
{
    public Guid Id { get; set; }
    public Guid PackageId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}
