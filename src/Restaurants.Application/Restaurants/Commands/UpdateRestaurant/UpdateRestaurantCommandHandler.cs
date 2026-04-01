using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Entities;

using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Constants;
namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,
 IRestaurantsRepository RestaurantsRepository,IMapper mapper,
 IRestaurantAuthorizationService authorizationService) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updateing restaurant by id:{retaurantid} with {@Rrestaurant}",request.Id,request);

        var restaurant  = await RestaurantsRepository.GetAsync(request.Id);
        if (restaurant is null)
            throw new NotFoundException(nameof(Restaurant),request.Id.ToString());

            if (!authorizationService.Authorize(restaurant, ResourceOperation.Update))
        {
            throw new ForbidException();
        }

        mapper.Map(request,restaurant);

        await RestaurantsRepository.SaveChanges();
        
       
         
    }
}

