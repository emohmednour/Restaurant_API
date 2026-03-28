using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Conmmands.DeleteDishes;

public class DeleteDishesForRestaurantCommandHandler
    (ILogger<DeleteDishesForRestaurantCommand> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository,
     IRestaurantAuthorizationService authorizationService) : IRequestHandler<DeleteDishesForRestaurantCommand>
{
    public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
    {


        logger.LogWarning("Deleting Dishes  for restaurant with id : {restaurantid}", request.RestaurantId);


        var restaurant = await restaurantsRepository.GetAsync(request.RestaurantId)
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        if (!authorizationService.Authorize(restaurant, ResourceOperation.Update))
        {
            throw new ForbidException();
        }


        await dishesRepository.DeleteRangeAsync(restaurant.Dishes);




    }
}
