using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishForRestaurant;

public class GetDishForRestaurantQueryHandler(ILogger<GetDishForRestaurantQueryHandler> logger,
    IRestaurantsRepository restaurantsRepository, IMapper mapper) : IRequestHandler<GetDishForRestaurantQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving dish id: {@dishid} for restaurant id : {@restaurantid}",
            request.DishId,
            request.RestaurantId
            );
        var restaurant = await restaurantsRepository.GetAsync(request.RestaurantId)
          ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dish = restaurant.Dishes.FirstOrDefault(d=>d.Id == request.DishId)
            ?? throw new NotFoundException(nameof(Dish),request.DishId.ToString());
        var result = mapper.Map<DishDto>(dish);

        return result;

    }
}
