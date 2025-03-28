using DukandaCore.Application.News.Commands.CreateNews;
using DukandaCore.Application.News.Commands.CreateNewsImages;
using DukandaCore.Application.News.Commands.DeleteNews;
using DukandaCore.Application.News.Commands.PublishNews;
using DukandaCore.Application.News.Commands.RemoveNewsImage;
using DukandaCore.Application.News.Commands.SetNewsFeatured;
using DukandaCore.Application.News.Commands.UpdateNews;
using DukandaCore.Application.News.Queries.GetFeaturedNews;
using DukandaCore.Application.News.Queries.GetNews;
using DukandaCore.Application.News.Queries.GetNewsDetails;
using Microsoft.AspNetCore.Mvc;

namespace DukandaCore.Web.Controllers;

public class NewsController : BaseController
{
    public NewsController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromForm] CreateNewsCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateNewsCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteNewsCommand { Id = id };
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return NoContent();
    }

    [HttpPost("{id}/publish")]
    public async Task<ActionResult> Publish(Guid id)
    {
        var command = new PublishNewsCommand { Id = id };
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpPatch("{id}/featured")]
    public async Task<ActionResult> SetFeatured(Guid id, SetNewsFeaturedCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpGet]
    public async Task<ActionResult> GetNews([FromQuery] GetNewsQuery query)
    {
        var result = await _sender.Send(query);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetNewsDetails(Guid id)
    {
        var query = new GetNewsDetailsQuery { NewsId = id };
        var result = await _sender.Send(query);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpGet("featured")]
    public async Task<ActionResult> GetFeatured()
    {
        var result = await _sender.Send(new GetFeaturedNewsQuery());
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    // Gallery Endpoints
    [HttpPost("{newsId}/gallery")]
    public async Task<ActionResult> AddGalleryImage(Guid newsId, [FromForm] CreateNewsImagesCommand command)
    {
        if (newsId != command.NewsId)
            return BadRequest();

        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result);
    }

    [HttpDelete("{newsId}/gallery/{imageId}")]
    public async Task<ActionResult> RemoveGalleryImage(Guid newsId, Guid imageId)
    {
        var command = new RemoveNewsImageCommand
        {
            NewsId = newsId,
            ImageId = imageId
        };

        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return NoContent();
    }
} 