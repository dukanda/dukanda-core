using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Enums;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using Microsoft.Extensions.Logging;

public record CancelBookingCommand : IRequest<Result<BookingDto>>
{
    public Guid BookingId { get; init; }
}

public class CancelBookingCommandHandler : IRequestHandler<CancelBookingCommand, Result<BookingDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<CancelBookingCommandHandler> _logger;
    private readonly IUser _currentUser;

    public CancelBookingCommandHandler(
        IApplicationDbContext context,
        ILogger<CancelBookingCommandHandler> logger,
        IUser currentUser)
    {
        _context = context;
        _logger = logger;
        _currentUser = currentUser;
    }

    public async Task<Result<BookingDto>> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.Id == request.BookingId, cancellationToken);

        if (booking == null)
        {
            return Result.Failure<BookingDto>(ErrorCodes.BookingNotFound);
        }

        // Check if user has permission to cancel
        if (booking.UserId != _currentUser.Id && !_currentUser.IsInRole("Admin"))
        {
            return Result.Failure<BookingDto>(ErrorCodes.ResourceNotFound);
        }

        // Check if booking can be cancelled
        if (booking.BookingStatusId == (int)BookingStatusEnum.Completed ||
            booking.BookingStatusId == (int)BookingStatusEnum.Cancelled)
        {
            _logger.LogWarning("Cannot cancel booking {BookingId} with current status", request.BookingId);
            return Result.Failure<BookingDto>(ErrorCodes.InvalidBookingStatus);
        }

        // Update booking status
        booking.BookingStatusId = (int)BookingStatusEnum.Cancelled;

        // Restore available slots
        foreach (var item in booking.Items)
        {
            var package = await _context.Packages.FindAsync(item.PackageId);
            package!.AvailableSlots += item.Quantity;
            package.Tour.AvailableSlots += item.Quantity;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(new BookingDto(booking));
    }
}