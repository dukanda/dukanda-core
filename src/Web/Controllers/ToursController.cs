using DukandaCore.Application.Tours.Commands.AddPackage;
using DukandaCore.Application.Tours.Commands.AddTourAttraction;
using DukandaCore.Application.Tours.Commands.RemovePackage;
using DukandaCore.Application.Tours.Commands.RemoveTourAttraction;
using DukandaCore.Application.Tours.Commands.RemoveTourItinerary;
using DukandaCore.Application.Tours.Commands.CreateTourImages;
using DukandaCore.Application.Tours.Commands.RemoveTourImage;
using DukandaCore.Application.Tours.Queries.SearchTours;
using DukandaCore.Application.Tours.Queries.CheckTourAvailability;
using DukandaCore.Application.Tours.Commands.PublishTour;
using DukandaCore.Application.Tours.Queries.GetAgencyTours;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
    public async Task<ActionResult> PublishTour(Guid id)
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
    public async Task<ActionResult> GetFeaturedTours()
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

    [HttpPost("packages")]
    public async Task<ActionResult> AddPackage(
        [FromBody] AddPackageCommand command)
    {
        // if (tourId != command.TourId)
        //     return BadRequest();

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

    // Gallery Endpoints
    [HttpPost("{tourId}/gallery")]
    public async Task<ActionResult> AddGalleryImage(Guid tourId, [FromForm] CreateTourImagesCommand command)
    {
        if (tourId != command.TourId)
            return BadRequest();

        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result);
    }

    [HttpDelete("{tourId}/gallery/{imageId}")]
    public async Task<ActionResult> RemoveGalleryImage(Guid tourId, Guid imageId)
    {
        var command = new RemoveTourImageCommand
        {
            TourId = tourId,
            ImageId = imageId
        };

        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<ActionResult> SearchTours([FromQuery] SearchToursQuery query)
    {
        var result = await _sender.Send(query);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpGet("{id}/availability")]
    public async Task<ActionResult> CheckAvailability(Guid id, [FromQuery] CheckTourAvailabilityQuery query)
    {
        if (id != query.TourId)
            return BadRequest();

        var result = await _sender.Send(query);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpGet("my-agency")]
    [Authorize]
    public async Task<ActionResult> GetMyAgencyTours([FromQuery] GetAgencyToursQuery query)
    {
        var result = await _sender.Send(query);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }
}