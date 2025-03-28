using Microsoft.AspNetCore.Mvc;

namespace DukandaCore.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController(ISender sender) : ControllerBase
{
    protected readonly ISender _sender = sender;
}