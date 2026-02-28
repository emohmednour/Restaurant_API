using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Conmmands.CreateDish;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Dishes.Queries.GetAllDishesForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishForRestaurant;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants/{restaurantId}/dishes")]
    public class DishesController(IMediator mediator) : Controller
    {
        [HttpPost]
        public  async Task<IActionResult> Create([FromRoute]int restaurantId, CreateDishCommand command)
        {
            command.ResturantID = restaurantId;
            await mediator.Send(command);

            return Created();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant([FromRoute] int restaurantId) {


            var result =  await mediator.Send(new GetAllDishesForRestaurantQuery(restaurantId));
            return Ok(result);
        
        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDto>> GetAllForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId) {


            var result =  await mediator.Send(new GetDishForRestaurantQuery(restaurantId,dishId));
            return Ok(result);
        
        }
          
    }
}
