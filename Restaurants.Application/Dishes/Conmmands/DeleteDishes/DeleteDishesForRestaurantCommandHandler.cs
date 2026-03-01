using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Conmmands.DeleteDishes;

public class DeleteDishesForRestaurantCommandHandler
    (ILogger<DeleteDishesForRestaurantCommand> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository): IRequestHandler<DeleteDishesForRestaurantCommand>
{
    public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
    {

        logger.LogInformation("Deleting Dishes  for restaurant with id : {restaurantid}", request.RestaurantId);

        var restaurant = await restaurantsRepository.GetAsync(request.RestaurantId)
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            

        await dishesRepository.DeleteRangeAsync(restaurant.Dishes);




    }
}
