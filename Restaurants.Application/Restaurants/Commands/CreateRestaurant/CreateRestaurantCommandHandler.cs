 using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Application.Users; 

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger
    , IMapper mapper,
    IRestaurantsRepository RestaurantsRepository,IUserContext usercontext):IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var user = usercontext.GetCurrentUser();
        logger.LogInformation("User:{Email} with id [{userId}] Creating restaurant {@Restaurant}",user.Email,user.Id,request);
        var restaurant = mapper.Map<Restaurant>(request);
        restaurant.OwnerId = user.Id;
        var Id = await RestaurantsRepository.Create(restaurant);
        return Id;
    }
}
