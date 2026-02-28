using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Application.Restaurants.Queries.GetRestaurant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger,
    IMapper mapper,
    IRestaurantsRepository RestaurantsRepository)
    : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
{
   

    public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation( "Getting restaurant {RestaurantId}" ,request.Id);

        var restaurant = await RestaurantsRepository.GetAsync(request.Id) ??
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        var restaurantDTO = mapper.Map<RestaurantDto>(restaurant);
            

        return restaurantDTO;
    }
}
