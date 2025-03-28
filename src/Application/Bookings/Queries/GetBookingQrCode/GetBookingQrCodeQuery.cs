using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;

namespace DukandaCore.Application.Bookings.Queries.GetBookingQrCode;

public record GetBookingQrCodeQuery : IRequest<Result<string>>
{
    public Guid BookingId { get; init; }
}

public class GetBookingQrCodeQueryHandler : IRequestHandler<GetBookingQrCodeQuery, Result<string>>
{
    private readonly IApplicationDbContext _context;
    private readonly IQrCodeService _qrCodeService;

    public GetBookingQrCodeQueryHandler(IApplicationDbContext context, IQrCodeService qrCodeService)
    {
        _context = context;
        _qrCodeService = qrCodeService;
    }

    public async Task<Result<string>> Handle(GetBookingQrCodeQuery request, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings.FindAsync(request.BookingId);
        
        if (booking == null)
            return Result.Failure<string>(ErrorCodes.ResourceNotFound);

        if (string.IsNullOrEmpty(booking.QrCodeUrl))
        {
            booking.QrCodeUrl = await _qrCodeService.GenerateQrCodeAsBase64(booking.BookingNumber);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Result.Success(booking.QrCodeUrl);
    }
} 