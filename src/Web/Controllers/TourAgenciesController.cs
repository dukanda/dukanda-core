using DukandaCore.Application.Common.Models;
using DukandaCore.Application.TourAgencies.Commands.CreateTourAgency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DukandaCore.Web.Controllers;

public class TourAgenciesController : BaseController
{
    public TourAgenciesController(ISender sender) : base(sender) { }

    [HttpPost]
    public async Task<ActionResult<TourAgencyDto>> Create([FromForm] CreateTourAgencyCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<TourAgencyDto>>> GetAll([FromQuery] GetTourAgenciesQuery query)
    {
        var result = await _sender.Send(query);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }


[HttpGet("{id}")]
public async Task<ActionResult<Result<TourAgencyDto>>> GetById(Guid id)
{
    var result = await _sender.Send(new GetTourAgencyByIdQuery(id));
    if (!result.IsSuccess)
        return BadRequest(result.Error);
    return Ok(result.Data);
}

[HttpPut("{id}")]
public async Task<ActionResult<Result<TourAgencyDto>>> Update(Guid id, UpdateTourAgencyCommand command)
{
    if (id != command.UserId)
        return BadRequest();
        
    var result = await _sender.Send(command);
    if (!result.IsSuccess)
        return BadRequest(result.Error);
    return Ok(result.Data);
}

[HttpPut("{id}/logo")]
public async Task<ActionResult<Result<string>>> UpdateLogo(Guid id, [FromForm] UpdateAgencyLogoCommand command)
{
    if (id != command.UserId)
        return BadRequest();
        
    var result = await _sender.Send(command);
    if (!result.IsSuccess)
        return BadRequest(result.Error);
    return Ok(result.Data);
}

[HttpPut("{id}/verify")]
[Authorize(Roles = "Admin")]
public async Task<ActionResult<Result>> VerifyAgency(Guid id, VerifyAgencyCommand command)
{
    if (id != command.UserId)
        return BadRequest();
        
    var result = await _sender.Send(command);
    if (!result.IsSuccess)
        return BadRequest(result.Error);
    return Ok();
}
}