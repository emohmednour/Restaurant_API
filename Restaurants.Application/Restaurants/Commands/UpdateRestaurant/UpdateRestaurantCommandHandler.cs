using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Exceptions;
namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,
 IRestaurantRepository restaurantRepository,IMapper mapper) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updateing restaurant by id:{retaurantid} with {@Rrestaurant}",request.Id,request);

        var restaurant  = await restaurantRepository.GetAsync(request.Id);
        if (restaurant is null)
            throw new NotFoundException($"Something errors with {request.Id}");

        mapper.Map(request,restaurant);

        await restaurantRepository.SaveChanges();
        
       
         
    }
}

