using DukandaCore.Application.Banners.Commands.CreateBanner;
using DukandaCore.Application.Banners.Commands.DeleteBanner;
using DukandaCore.Application.Banners.Commands.UpdateBanner;
using DukandaCore.Application.Banners.Queries.GetBannerDetails;
using DukandaCore.Application.Banners.Queries.GetBanners;
using Microsoft.AspNetCore.Mvc;

namespace DukandaCore.Web.Controllers;

public class BannersController : BaseController
{
    public BannersController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromForm] CreateBannerCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromForm] UpdateBannerCommand command)
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
        var command = new DeleteBannerCommand { Id = id };
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult> GetBanners([FromQuery] GetBannersQuery query)
    {
        var result = await _sender.Send(query);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetBannerDetails(Guid id)
    {
        var query = new GetBannerDetailsQuery { Id = id };
        var result = await _sender.Send(query);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }
} 