using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,
 IRestaurantRepository restaurantRepository,IMapper mapper) : IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updateing restaurant by id:{retaurantid} with {@Rrestaurant}",request.Id,request);

        var restaurant  = await restaurantRepository.GetAsync(request.Id);
        if(restaurant is null)
            return false;

        mapper.Map(request,restaurant);

        await restaurantRepository.SaveChanges();
        
        return true;
         
    }
}

