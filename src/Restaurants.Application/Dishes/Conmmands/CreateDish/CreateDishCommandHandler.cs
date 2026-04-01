using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Dishes.Conmmands.CreateDish;

public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
    IMapper mapper,IRestaurantsRepository RestaurantsRepository,
    IDishesRepository dishesRepository,
    IRestaurantAuthorizationService authorizationService) : IRequestHandler<CreateDishCommand,int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating Dish  {@Dish}",request);

        var restaurant = await RestaurantsRepository.GetAsync(request.ResturantID)
            ?? throw new NotFoundException(nameof(Restaurant), request.ResturantID.ToString());


 if (!authorizationService.Authorize(restaurant, ResourceOperation.Update))
        {
            throw new ForbidException();
        }
            

        var dish = mapper.Map<Dish>(request);

        return  await dishesRepository.Create(dish);
    }
}
