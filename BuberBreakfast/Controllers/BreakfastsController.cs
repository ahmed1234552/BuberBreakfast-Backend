using Microsoft.AspNetCore.Mvc;//TO BE ABLE TO INHIRIT FROM ControllerBase
using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.Services.Breakfast;


namespace BuberBreakfast.Controllers;
[ApiController]//attribute
[Route("[breakfasts]")]//to be able to use the endpoints here directly without /breakfasts/
//or [Route("[controller]")]//to be able to use the endpoints here directly without /breakfasts/
//but it will add the name of the class below without controller
public class BreakfastController : ControllerBase
{
    private readonly IBreakfastService _breakfastService;

    public BreakfastController(IBreakfastService breakfastService)
    {
        _breakfastService = breakfastService;
    }

    //endpoints
    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        var breakfast = new Breakfast(
            Guid.NewGuid(),//added id
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,//last modified date time
            request.Savory,
            request.Sweet
        );
        //save breakfast to database
        _breakfastService.CreateBreakfast(breakfast);

        var response = new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );

        return CreatedAtAction(
            actionName:nameof(GetBreakfast),//the end point
            routeValues:new { id = breakfast.Id },
            value:response

        );
        //return Ok(response);//Ok(response); 200 ok because we use the basemethod
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        return Ok(id);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id,UpsertBreakfastRequest request)
    {
        return Ok(request);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeletreBreakfast(Guid id)
    {
        return Ok(id);
    }
}