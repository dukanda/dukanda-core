using DukandaCore.Application.Common.Models;

namespace DukandaCore.Application.Bookings.Queries.GetUserBookings;

public record GetUserBookingsQuery : IRequest<Result<List<BookingDto>>>
{
    public Guid UserId { get; init; }
    public DateTime? FromDate { get; init; }
    public DateTime? ToDate { get; init; }
} 