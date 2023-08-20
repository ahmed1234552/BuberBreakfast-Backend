using Microsoft.AspNetCore.Mvc;//TO BE ABLE TO INHIRIT FROM ControllerBase

namespace BuberBreakfast.Controllers;

[ApiController]//attribute
public class BreakfastController : ControllerBase
{
    //endpoints
    [HttpPost("/breakfasts")]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        return Ok();
    }
}