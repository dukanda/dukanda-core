using DukandaCore.Application.Tours.Dtos;
using DukandaCore.Domain.Entities;

public class BookingDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string BookingNumber { get; set; }
    public int BookingStatusId { get; set; }
    public string BookingStatusName { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTimeOffset Created { get; set; }

    public BookingDto(Booking booking)
    {
        Id = booking.Id;
        UserId = booking.UserId;
        BookingNumber = booking.BookingNumber;
        BookingStatusId = booking.BookingStatusId;
        BookingStatusName = booking.BookingStatus.Name;
        TotalPrice = booking.TotalPrice;
        Created = booking.Created;
    }
}
