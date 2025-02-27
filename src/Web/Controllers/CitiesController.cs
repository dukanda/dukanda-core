using Microsoft.AspNetCore.Mvc;

namespace DukandaCore.Web.Controllers;

public class CitiesController : BaseController
{
    public CitiesController(ISender sender) : base(sender)
    {
    }
    [HttpGet]
    public async Task<ActionResult> GetCities([FromQuery] GetCitiesQuery query)
    {
        var result = await _sender.Send(query);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }
}
