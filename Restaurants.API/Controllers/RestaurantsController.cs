using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.DTOs;
using MediatR;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurant;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
namespace Restaurants.API.Controllers
{

    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rest = await mediator.Send(new GetAllRestaurantsQuery());
            return Ok(rest);

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute]int Id)
        {

            var rest = await mediator.Send(new GetRestaurantByIdQuery(Id));
            if (rest is null)
                return NotFound();

            return Ok(rest);
        }


        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand restaurantCommand  )
        {
            if (restaurantCommand is null)
                return NotFound();


            var Id = await mediator.Send(restaurantCommand);
            return CreatedAtAction(nameof(GetById), new { id = Id }, null);

        }
    }
}
