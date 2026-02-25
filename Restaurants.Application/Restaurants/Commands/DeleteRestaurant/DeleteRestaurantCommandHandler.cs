using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,IRestaurantRepository restaurantRepository)
    : IRequestHandler<DeleteRestaurantCommand, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting restaurant with id {Restaurantid}", request.Id);
        var rest = await restaurantRepository.GetAsync(request.Id);

        if(rest is null)
            return false;
        await restaurantRepository.Delete(rest);
        return true;
        
    }
}
