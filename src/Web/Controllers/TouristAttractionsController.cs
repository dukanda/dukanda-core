using DukandaCore.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace DukandaCore.Web.Controllers;

public class TouristAttractionsController(ISender sender) : BaseController(sender)
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<TouristAttractionDto>>> Get(
        [FromQuery] GetTouristAttractionsQuery query)
    {
        var result = await _sender.Send(query);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TouristAttractionDto>> GetById(Guid id)
    {
        var result = await _sender.Send(new GetTouristAttractionByIdQuery(id));
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<ActionResult<TouristAttractionDto>> Create(
        [FromForm] CreateTouristAttractionCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TouristAttractionDto>> Update(Guid id,
        [FromForm] UpdateTouristAttractionCommand command)
    {
        if (id != command.Id) return BadRequest();
        var result = await _sender.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result>> Delete(Guid id)
    {
        var result = await _sender.Send(new DeleteTouristAttractionCommand(id));
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return NoContent();
    }

    [HttpGet("featured")]
    public async Task<ActionResult<List<TouristAttractionDto>>> GetFeatured()
    {
        var result = await _sender.Send(new GetFeaturedTouristAttractionsQuery());
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }
    [HttpPost("{touristAttractionId}/images")]
    public async Task<ActionResult> CreateImages(
        Guid touristAttractionId,
        [FromForm] CreateAttractionImagesCommand command)
    {
        if (touristAttractionId != command.TouristAttractionId)
            return BadRequest();

        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Created();
    }
}