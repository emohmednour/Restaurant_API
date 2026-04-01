using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Constants;
namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,
    IRestaurantsRepository RestaurantsRepository,
    IRestaurantAuthorizationService authorizationService)
    : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting restaurant with id {Restaurantid}", request.Id);
        var restaurant = await RestaurantsRepository.GetAsync(request.Id);

        if(restaurant is null)
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

            if (!authorizationService.Authorize(restaurant, ResourceOperation.Delete))
        {
            throw new ForbidException();
        }
        await RestaurantsRepository.Delete(restaurant);
       
        
    }
}
