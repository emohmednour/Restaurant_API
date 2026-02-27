using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.DTOs;
using MediatR;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurant;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
namespace Restaurants.API.Controllers
{

    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
        {
            
            var rest = await mediator.Send(new GetAllRestaurantsQuery());
            return Ok(rest);

        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<RestaurantDto>> GetById([FromRoute]int Id)
        {
            try
            {
                var rest = await mediator.Send(new GetRestaurantByIdQuery(Id));
                

                return Ok(rest);

            }
            catch (Exception ex)
            {
                return StatusCode(500,"Some thing error");

            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand restaurantCommand  )
        {
            if (restaurantCommand is null)
                return NotFound();


            var Id = await mediator.Send(restaurantCommand);
            return CreatedAtAction(nameof(GetById), new { id = Id }, null);

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            await mediator.Send(new DeleteRestaurantCommand(id));

            
                return NoContent();

            

        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, UpdateRestaurantCommand command)
        {

            command.Id = id;
            await mediator.Send(command);
            
                return NoContent();

          
        }



    }
}
