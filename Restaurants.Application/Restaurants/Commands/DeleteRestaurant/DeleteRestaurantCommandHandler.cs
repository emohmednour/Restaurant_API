using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,
    IRestaurantsRepository RestaurantsRepository)
    : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting restaurant with id {Restaurantid}", request.Id);
        var rest = await RestaurantsRepository.GetAsync(request.Id);

        if(rest is null)
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        await RestaurantsRepository.Delete(rest);
       
        
    }
}
