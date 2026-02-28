using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetAllDishesForRestaurant;

public class GetAllDishesForRestaurantQueryHandler (
    ILogger<GetAllDishesForRestaurantQueryHandler> logger,
    IRestaurantsRepository restaurantsRepository,IMapper mapper
    )
    : IRequestHandler<GetAllDishesForRestaurantQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetAllDishesForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving dishes for restaurant id : {@restaurantid}",request.RestauratId);

        var restaurant = await restaurantsRepository.GetAsync(request.RestauratId)
          ?? throw new NotFoundException(nameof(Restaurant), request.RestauratId.ToString());

        //var dishes = restaurant.Dishes;

        var results = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);

        return results;




    }
}
