using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Domain.Entities;

namespace DukandaCore.Application.Bookings.Commands.CreateBooking;
using DukandaCore.Application.Common.Enums;

public record CreateBookingCommand : IRequest<Result<BookingDto>>
{
    public List<BookingItemRequest> Items { get; init; } = new();
    public int PaymentMethodId { get; init; }
}

public record BookingItemRequest
{
    public Guid PackageId { get; init; }
    public int Quantity { get; init; }
}
public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Result<BookingDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _currentUser;

    public CreateBookingCommandHandler(IApplicationDbContext context, IUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Result<BookingDto>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        // 1. Validate packages and slots availability
        foreach (var item in request.Items)
        {
            var package = await _context.Packages
                .Include(p => p.Tour)
                .FirstOrDefaultAsync(p => p.Id == item.PackageId);

            if (package == null)
                return Result.Failure<BookingDto>(ErrorCodes.PackageNotFound);

            if (package.AvailableSlots < item.Quantity)
                return Result.Failure<BookingDto>(ErrorCodes.InsufficientSlots);
        }

        // 2. Create booking
        var booking = new Booking
        {
            UserId = _currentUser.Id!.Value,
            BookingNumber = Guid.NewGuid().ToString("N"),
            BookingStatusId = (int)BookingStatusEnum.Pending,
            ExpirationDate = DateTimeOffset.UtcNow.AddMinutes(30) // Give 30 minutes to complete payment
        };

        // 3. Add booking items
        foreach (var item in request.Items)
        {
            var package = await _context.Packages.FindAsync(item.PackageId);
            var bookingItem = new BookingItem
            {
                PackageId = item.PackageId,
                Quantity = item.Quantity,
                UnitPrice = package!.Price,
                TotalPrice = package.Price * item.Quantity
            };
            booking.Items.Add(bookingItem);
            booking.TotalPrice += bookingItem.TotalPrice;

            // Update available slots
            package.AvailableSlots -= item.Quantity;
            package.Tour.AvailableSlots -= item.Quantity;
        }

        // 4. Create payment intent directly
        var paymentIntent = new PaymentIntent
        {
            BookingId = booking.Id,
            PaymentMethodId = request.PaymentMethodId,
            Amount = booking.TotalPrice,
            TransactionReference = GenerateTransactionReference(),
            CreatedAt = DateTime.UtcNow
        };

        booking.PaymentIntent = paymentIntent;

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(new BookingDto(booking));
    }

    private string GenerateTransactionReference()
    {
        return Guid.NewGuid().ToString("N").Substring(0, 12).ToUpper();
    }
}
