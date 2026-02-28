using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Conmmands.CreateDish;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/dishes/{restaurantId}/dishes")]
    public class DishesController(IMediator mediator) : Controller
    {
        [HttpPost]
        public  async Task<IActionResult> Create([FromRoute]int restaurantId, CreateDishCommand command)
        {
            command.ResturantID = restaurantId;
            await mediator.Send(command);

            return Created();
        }
    }
}
