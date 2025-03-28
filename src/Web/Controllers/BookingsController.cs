using DukandaCore.Application.Bookings.Commands.CreateBooking;
using DukandaCore.Application.Bookings.Commands.ConfirmBooking;
using DukandaCore.Application.Bookings.Commands.RefundBooking;
using DukandaCore.Application.Bookings.Queries.GetBookingQrCode;
using DukandaCore.Application.Bookings.Queries.GetUserBookings;
using Microsoft.AspNetCore.Mvc;

namespace DukandaCore.Web.Controllers;

public class BookingsController : BaseController
{
    public BookingsController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async Task<ActionResult> CreateBooking(CreateBookingCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpGet]
    public async Task<ActionResult> GetBookings([FromQuery] GetBookingsQuery query)
    {
        var result = await _sender.Send(query);
      
        return Ok(result.Data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetBookingDetails(Guid id)
    {
        var query = new GetBookingDetailsQuery { BookingId = id };
        var result = await _sender.Send(query);
        
        return Ok(result.Data);
    }

    [HttpPatch("{id}/cancel")]
    public async Task<ActionResult> CancelBooking(Guid id)
    {
        var command = new CancelBookingCommand { BookingId = id };
        var result = await _sender.Send(command);
        return Ok(result.Data);
    }

    [HttpPost("{id}/confirm")]
    public async Task<ActionResult> ConfirmBooking(Guid id, ConfirmBookingCommand command)
    {
        if (id != command.BookingId)
            return BadRequest();

        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpGet("my-bookings")]
    public async Task<ActionResult> GetMyBookings([FromQuery] GetUserBookingsQuery query)
    {
        var result = await _sender.Send(query);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpGet("{id}/qr-code")]
    public async Task<ActionResult> GetBookingQrCode(Guid id)
    {
        var query = new GetBookingQrCodeQuery { BookingId = id };
        var result = await _sender.Send(query);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpPost("{id}/refund")]
    public async Task<ActionResult> RefundBooking(Guid id, RefundBookingCommand command)
    {
        if (id != command.BookingId)
            return BadRequest();

        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }
}
