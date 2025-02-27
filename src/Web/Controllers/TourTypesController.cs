using Microsoft.AspNetCore.Mvc;

namespace DukandaCore.Web.Controllers;

public class TourTypesController : BaseController
{
    public TourTypesController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<ActionResult> GetTourTypes([FromQuery] GetTourTypesQuery query)
    {
        var result = await _sender.Send(query);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }
}
