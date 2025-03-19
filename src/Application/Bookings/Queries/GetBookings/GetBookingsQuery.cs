using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using Microsoft.Extensions.Logging;

public record GetBookingsQuery : IRequest<Result<List<BookingDto>>>
{
    // Filtering options
    public Guid? UserId { get; init; }
    public DateTimeOffset? StartDate { get; init; }
    public DateTimeOffset? EndDate { get; init; }
    public int? StatusId { get; init; }
}

public class GetBookingsQueryHandler : IRequestHandler<GetBookingsQuery, Result<List<BookingDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetBookingsQueryHandler> _logger;
    private readonly IUser _currentUser;

    public GetBookingsQueryHandler(
        IApplicationDbContext context, 
        ILogger<GetBookingsQueryHandler> logger,
        IUser currentUser)
    {
        _context = context;
        _logger = logger;
        _currentUser = currentUser;
    }

    public async Task<Result<List<BookingDto>>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
    {

            var query = _context.Bookings
                .Include(b => b.Items)
                .Include(b => b.PaymentIntent)
                .AsQueryable();

            // Apply filters
            if (request.UserId.HasValue)
                query = query.Where(b => b.UserId == request.UserId.Value);

            if (request.StartDate.HasValue)
                query = query.Where(b => b.Created >= request.StartDate.Value);

            if (request.EndDate.HasValue)
                query = query.Where(b => b.Created <= request.EndDate.Value);

            if (request.StatusId.HasValue)
                query = query.Where(b => b.BookingStatusId == request.StatusId.Value);

            var bookings = await query
                .OrderByDescending(b => b.Created)
                .Take(100) // Limit to prevent large result sets
                .ToListAsync(cancellationToken);

            var bookingDtos = bookings.Select(b => new BookingDto(b)).ToList();

            return Result.Success(bookingDtos);
       
    }
}
