using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Conmmands.CreateDish;

public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
    IMapper mapper,IRestaurantsRepository RestaurantsRepository,
    IDishesRepository dishesRepository) : IRequestHandler<CreateDishCommand,int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating Dish  {@Dish}",request);

        var restaurant = await RestaurantsRepository.GetAsync(request.ResturantID)
            ?? throw new NotFoundException(nameof(Restaurant), request.ResturantID.ToString());


        var dish = mapper.Map<Dish>(request);

        return  await dishesRepository.Create(dish);
    }
}
