using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Resturants;
using Restaurants.Application.Resturants.DTOs;

namespace Restaurants.API.Controllers
{

    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController(IRestaurantService resturantService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rest = await resturantService.GetAllResturants();
            return Ok(rest);

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute]int Id)
        {
            
            var rest = await resturantService.GetRestaurant(Id);
            if (rest is null)
                return NotFound();

            return Ok(rest);
        }


        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto restaurantDto  )
        {
            if (restaurantDto is null)
                return NotFound();
            var Id = await resturantService.Create(restaurantDto);
            return CreatedAtAction(nameof(GetById), new { id = Id }, null);

        }
    }
}
