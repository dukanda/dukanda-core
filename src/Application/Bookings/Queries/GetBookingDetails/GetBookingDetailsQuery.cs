using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using Microsoft.Extensions.Logging;

public record GetBookingDetailsQuery : IRequest<Result<BookingDto>>
{
    public Guid BookingId { get; init; }
}

public class GetBookingDetailsQueryHandler : IRequestHandler<GetBookingDetailsQuery, Result<BookingDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetBookingDetailsQueryHandler> _logger;
    private readonly IUser _currentUser;

    public GetBookingDetailsQueryHandler(
        IApplicationDbContext context, 
        ILogger<GetBookingDetailsQueryHandler> logger,
        IUser currentUser)
    {
        _context = context;
        _logger = logger;
        _currentUser = currentUser;
    }

    public async Task<Result<BookingDto>> Handle(GetBookingDetailsQuery request, CancellationToken cancellationToken)
    {

            var booking = await _context.Bookings
                .Include(b => b.Items)
                    .ThenInclude(i => i.Package)
                .Include(b => b.PaymentIntent)
                .FirstOrDefaultAsync(b => b.Id == request.BookingId, cancellationToken);

            if (booking == null)
            {
                _logger.LogWarning("Booking {BookingId} not found", request.BookingId);
                return Result.Failure<BookingDto>(ErrorCodes.BookingNotFound);
            }

            // Optional: Add authorization check
            if (booking.UserId != _currentUser.Id && !_currentUser.IsInRole("Admin"))
            {
                _logger.LogWarning("Unauthorized access to booking {BookingId}", request.BookingId);
                return Result.Failure<BookingDto>(ErrorCodes.ResourceNotFound);
            }

            var bookingDto = new BookingDto(booking);

            _logger.LogInformation("Retrieved details for booking {BookingId}", request.BookingId);

            return Result.Success(bookingDto);
    }
}
