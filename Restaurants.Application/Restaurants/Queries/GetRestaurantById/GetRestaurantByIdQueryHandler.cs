using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Application.Restaurants.Queries.GetRestaurant;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger,
    IMapper mapper,
    IRestaurantRepository restaurantRepository)
    : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
{
   

    public async Task<RestaurantDto?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation(message: $"Getting restaurant {request.Id}");

        var restaurant = await restaurantRepository.GetAsync(request.Id);
        var restaurantDTO = mapper.Map<RestaurantDto>(restaurant);

        return restaurantDTO;
    }
}
