using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Entities;
namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,
 IRestaurantsRepoSitory RestaurantsRepository,IMapper mapper) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updateing restaurant by id:{retaurantid} with {@Rrestaurant}",request.Id,request);

        var restaurant  = await RestaurantsRepository.GetAsync(request.Id);
        if (restaurant is null)
            throw new NotFoundException(nameof(Restaurant),request.Id.ToString());

        mapper.Map(request,restaurant);

        await RestaurantsRepository.SaveChanges();
        
       
         
    }
}

