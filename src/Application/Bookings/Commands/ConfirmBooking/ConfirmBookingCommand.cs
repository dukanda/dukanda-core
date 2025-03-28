using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Domain.Entities;
using DukandaCore.Application.Common.Enums;

namespace DukandaCore.Application.Bookings.Commands.ConfirmBooking;

public record ConfirmBookingCommand : IRequest<Result<BookingDto>>
{
    public Guid BookingId { get; init; }
    public string PaymentIntentId { get; init; } = string.Empty;
}

public class ConfirmBookingCommandHandler : IRequestHandler<ConfirmBookingCommand, Result<BookingDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IQrCodeService _qrCodeService;

    public ConfirmBookingCommandHandler(IQrCodeService qrCodeService, IApplicationDbContext context)
    {
        _qrCodeService = qrCodeService;
        _context = context;
    }

    public async Task<Result<BookingDto>> Handle(ConfirmBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.Id == request.BookingId);

        if (booking == null)
            return Result.Failure<BookingDto>(ErrorCodes.ResourceNotFound);

        if (string.IsNullOrEmpty(booking.PaymentIntentId) || booking.PaymentIntentId != request.PaymentIntentId)
            return Result.Failure<BookingDto>(ErrorCodes.InvalidPayment);

        // Generate QR Code
        booking.QrCodeUrl = await _qrCodeService.GenerateQrCodeAsBase64(booking.BookingNumber);
        booking.BookingStatusId = (int)BookingStatusEnum.Confirmed;
        booking.PaymentDate = DateTimeOffset.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(new BookingDto(booking));
    }
} 