using DukandaCore.Application.Common.Models;

namespace DukandaCore.Application.Bookings.Commands.RefundBooking;

public record RefundBookingCommand : IRequest<Result<BookingDto>>
{
    public Guid BookingId { get; init; }
    public string Reason { get; init; } = string.Empty;
} 