using Microsoft.AspNetCore.Mvc;//TO BE ABLE TO INHIRIT FROM ControllerBase
using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.Services.Breakfasts;
using ErrorOr;
using BuberBreakfast.ServiceErrors;

namespace BuberBreakfast.Controllers;
[ApiController]//attribute
[Route("breakfasts")]//to be able to use the endpoints here directly without /breakfasts/
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
        ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakfast(id);
        if(
            getBreakfastResult.IsError &&
            getBreakfastResult.FirstError == Errors.Breakfast.NotFound
        )
        {
            return NotFound();
        }
        var breakfast = getBreakfastResult.Value;
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

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id,UpsertBreakfastRequest request)
    {
        var breakfast = new Breakfast(
            id,//added id
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,//last modified date time
            request.Savory,
            request.Sweet
        );

        _breakfastService.UpsertBreakfast(breakfast);
        // TODO: return 201 if a new breakfast was created
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeletreBreakfast(Guid id)
    {
        _breakfastService.DeleteBreakfast(id);
        return NoContent();
    }
}