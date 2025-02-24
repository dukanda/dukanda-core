using DukandaCore.Application.Common.Models;

public record CreateBookingCommand : IRequest<Result<BookingDto>>
{
    public Guid UserId { get; init; }
    public Guid PackageId { get; init; }
}
