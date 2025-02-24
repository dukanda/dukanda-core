using DukandaCore.Application.Tours.Commands.AddTourAttraction;
using DukandaCore.Application.Tours.Commands.RemovePackage;
using DukandaCore.Application.Tours.Commands.RemoveTourAttraction;
using DukandaCore.Application.Tours.Commands.RemoveTourItinerary;
using Microsoft.AspNetCore.Mvc;

namespace DukandaCore.Web.Controllers;

public class ToursController : BaseController
{
    public ToursController(ISender sender) : base(sender)
    {
    }


    [HttpPost]
    public async Task<ActionResult> Create([FromForm] CreateTourCommand command)
    {
        var request = Request;
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateTourCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpPatch("{id}/featured")]
    public async Task<ActionResult> SetFeatured(Guid id, SetTourFeaturedCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpPost("{id}/publish")]
    public async Task<ActionResult> Publish(Guid id)
    {
        var command = new PublishTourCommand { Id = id };
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpGet]
    public async Task<ActionResult> GetTours([FromQuery] GetToursQuery query)
    {
        query.IncludeUnpublishedTours();

        var result = await _sender.Send(query);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

[HttpGet("{id}")]
public async Task<ActionResult> GetTourDetails(Guid id)
{
    var query = new GetTourDetailsQuery { TourId = id };
    var result = await _sender.Send(query);

    if (!result.IsSuccess)
        return BadRequest(result.Error);

    return Ok(result.Data);
}


    [HttpGet("featured")]
    public async Task<ActionResult> GetFeatured()
    {
        var result = await _sender.Send(new GetFeaturedToursQuery());
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpGet("published")]
    public async Task<ActionResult> GetPublishedTours([FromQuery] GetToursQuery query)
    {
        var result = await _sender.Send(query);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpPost("{tourId}/packages")]
    public async Task<ActionResult> AddPackage(Guid tourId, AddPackageCommand command)
    {
        if (tourId != command.TourId)
            return BadRequest();

        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }
    [HttpDelete("packages/{packageId}")]
    public async Task<ActionResult> RemovePackage(Guid packageId)
    {
        var command = new RemovePackageCommand { PackageId = packageId };
        var result = await _sender.Send(command);
    
        if (!result.IsSuccess)
            return BadRequest(result.Error);
    
        return NoContent();
    }

    [HttpPost("bookings")]
    public async Task<ActionResult> CreateBooking(CreateBookingCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    // Itinerary Endpoints
    [HttpPost("{tourId}/itineraries")]
    public async Task<ActionResult> AddItinerary(Guid tourId, AddTourItineraryCommand command)
    {
        if (tourId != command.TourId)
            return BadRequest();

        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpDelete("itineraries/{itineraryId}")]
    public async Task<ActionResult> RemoveItinerary(Guid itineraryId)
    {
        var command = new RemoveTourItineraryCommand { ItineraryId = itineraryId };
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok();
    }

    // Attraction Endpoints
    [HttpPost("{tourId}/attractions")]
    public async Task<ActionResult> AddAttraction(Guid tourId, AddTourAttractionCommand command)
    {
        if (tourId != command.TourId)
            return BadRequest();

        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpDelete("{tourId}/attractions/{attractionId}")]
    public async Task<ActionResult> RemoveAttraction(Guid tourId, Guid attractionId)
    {
        var command = new RemoveTourAttractionCommand()
        {
            TourId = tourId,
            TouristAttractionId = attractionId
        };

        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok();
    }

}